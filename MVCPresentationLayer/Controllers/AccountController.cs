using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.Authentication;
using Contracts.ApplicationLayer.Interface;
using DomainLayer.Common;
using DomainLayer.Errors;
using InfrastructureLayer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MVCPresentationLayer.Extensions;
using MVCPresentationLayer.Helpers;
using MVCPresentationLayer.ViewModels;
using MVCPresentationLayer.ViewModels.Account;

namespace MVCPresentationLayer.Controllers
{
    public class AccountController : ControllerWithHelpers
    {
        private readonly IAccountService _authService;
        private readonly ILogger _logger;
        private readonly CaptchaOptions _captchaOptions;
        private readonly JwtOptions _jwtOptions;

        public AccountController(IAccountService authService, ILogger<AccountController> logger, IOptions<CaptchaOptions> captchaOptions, IOptions<JwtOptions> jwtOptions)
        {
            _authService = authService;
            _logger = logger;
            _captchaOptions = captchaOptions.Value;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpGet]
        public IActionResult Login(bool restrict = false)
        {
            if(restrict == true)
            {
                ViewBag.ErrorMessage = "You need to log in to your account to access the application";
            }
            ViewBag.CaptchaKey = _captchaOptions.ClientKey;
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LogInViewModel request)
        {
            ViewBag.CaptchaKey = _captchaOptions.ClientKey;
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = ModelState.GetFirstErrorMessage();
                    request.CaptchaToken = "";
                    return View(request);
                }


                var response = await _authService.Login(new LogInRequest { Email = request.Email, Password = request.Password, CaptchaToken = request.CaptchaToken });
                if (!response.IsSuccess)
                {
                    ViewBag.ErrorMessage = response.ServiceError?.Message;
                    request.CaptchaToken = "";
                    return View(request);
                }
                ;
                ViewBag.SuccessMessage = "Log in Successful";
                Response.Cookies.Append("jwt", response.Value!.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(_jwtOptions.AccessTokenExpiryDays)
                });
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Login));
            }
        }

        public  IActionResult ForgotPassword()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = ModelState.GetFirstErrorMessage();
                    return View(request);
                }


                var response = await _authService.ForgotPassword(new ForgotPasswordRequest { Email = request.Email, });
                if (!response.IsSuccess)
                {
                    ViewBag.ErrorMessage = response.ServiceError?.Message;
                    return View(request);
                }
                ;
                TempData["SuccessMessage"] = "A reset password email has been sent successfully. Please reset your password and log in.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(ForgotPassword));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = ModelState.GetFirstErrorMessage();
                    return View(request);
                }

                var response = await _authService.ResetPassword(new ResetPasswordRequest { Password = request.NewPassword, Token = request.Token });
                if (!response.IsSuccess)
                {
                    TempData["ErrorMessage"] = response.ServiceError?.Message + " Please request a new reset password.";
                    return RedirectToAction("ForgotPassword");
                }
                ;
                TempData["SuccessMessage"] = "Password Reset successfull. Please log in using your new password.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(ForgotPassword));
            }
        }


        public IActionResult ResetPassword([FromQuery] [Required(AllowEmptyStrings = false)] string token)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Your request is invalid. Please request a new reset password.";
                    return RedirectToAction("ForgotPassword");
                }

                var viewModel = new ResetPasswordViewModel { Token = token };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(ForgotPassword));
            }
        }

        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at ${nameof(AccountController)} in action ${action}");
            return GetUnknownErrorView();
        }

    }
}