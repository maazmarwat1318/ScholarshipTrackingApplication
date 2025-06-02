using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.Authentication;
using Contracts.ApplicationLayer.Interface;
using DomainLayer.Common;
using DomainLayer.Errors;
using InfrastructureLayer.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Extensions;
using WebAPI.ViewModels.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _authService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AccountController(IAccountService authService, ILogger<AccountController> logger, IMapper mapper)
        {
            _authService = authService;
            _logger = logger;
            _mapper = mapper;
        }

        
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LogInViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }

                var response = await _authService.Login(_mapper.Map<LogInRequest>(request));
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                ;
                return this.SuccessObjectToHttpResponse(response.Value!);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(Login));
            }
        }


        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel request)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return this.BadRequestErrorResponse();
                }


                var response = await _authService.ForgotPassword(_mapper.Map<ForgotPasswordRequest>(request));
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                return this.SuccessObjectToHttpResponse(response.Value!);
                ;
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(ForgotPassword));
            }
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.BadRequestErrorResponse();
                }

                var response = await _authService.ResetPassword(_mapper.Map<ResetPasswordRequest>(request));
                if (!response.IsSuccess)
                {
                    return this.ErrorToHttpResponse(response.ServiceError!);
                }
                ;

                return this.SuccessObjectToHttpResponse(response.Value!);
            }
            catch (Exception ex)
            {
                return OnUnknowException(ex, nameof(ResetPassword));
            }
        }


        private IActionResult OnUnknowException(Exception ex, string action)
        {
            _logger.LogError(ex, $"Unknown error occured at {nameof(AccountController)} in action {action}");
            return this.ErrorToHttpResponse(CommonErrorHelper.ServerError());
        }

    }
}