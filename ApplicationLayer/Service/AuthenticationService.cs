using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using ApplicationLayer.DTO.Authentication;
using ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Entity;
using DomainLayer.Errors.AuthenticationErrors;
using ApplicationLayer.DTO.Common;

namespace ApplicationLayer.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly ICrypterService _crypterService;
        private readonly IEmailService _emailService;
        private readonly ICaptchaVerificationService _captchaService;

        public AuthenticationService(IJwtService jwtService, IUserRepository userRepo, ICrypterService crypterService, IEmailService emailService, ICaptchaVerificationService captchaService)
        {
            _jwtService = jwtService;
            _userRepo = userRepo;
            _crypterService = crypterService;
            _emailService = emailService;
            _captchaService = captchaService;
        }

        public async Task<Response<LogInResponse>> Login(LogInRequest request)
        {
            var isCaptchaValid = await _captchaService.VerifyTokenAsync(request.CaptchaToken);
            if(isCaptchaValid != true)
            {
                return Response<LogInResponse>.Failure(AuthenticationErrorHelper.InvalidCaptchaError());
            }
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return Response<LogInResponse>.Failure(AuthenticationErrorHelper.InvalidCredentialsError());
            }

            if (!_crypterService.CompareHash(request.Password, user.Password))
            {
                return Response<LogInResponse>.Failure(AuthenticationErrorHelper.InvalidCredentialsError());
            }

            return Response<LogInResponse>.Success(new LogInResponse { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Role = user.Role, Token = _jwtService.GenerateAccessToken(user.Id, user.Email, user.Role) });
        }

        public async Task<Response<MessageResponse>> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return Response<MessageResponse>.Success(new MessageResponse());
            }

            var token = _jwtService.GenerateResetPasswordToken(user.Id);

            try
            {
                await _emailService.SendPasswordResetEmail(user.FirstName, user.Email, token);
            } catch(Exception)
            {
                return Response<MessageResponse>.Failure(AuthenticationErrorHelper.ResetPasswordEmailSentFailureError());
            }

            return Response<MessageResponse>.Success(new MessageResponse());
        }

        public async Task<Response<MessageResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var tokenValidationResult = _jwtService.VerifyToken(request.Token);

            if(!tokenValidationResult.IsSuccess)
            {
                return Response<MessageResponse>.Failure(tokenValidationResult.ServiceError!);
            }

            var id = _jwtService.GetClaimValue<int?>(tokenValidationResult.Value!, "id");
            if(id == null)
            {
                Response<MessageResponse>.Failure(AuthenticationErrorHelper.TokenInvalidError());
            }
            var newPasswordHashed = _crypterService.EncryptString(request.Password);
            await _userRepo.UpdateUserPasswordAsync((int)id!, newPasswordHashed);
            return Response<MessageResponse>.Success(new MessageResponse());
        }

    }
}
