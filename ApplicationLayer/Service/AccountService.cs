using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Errors.AuthenticationErrors;
using DomainLayer.DTO.Common;
using System.Security.Claims;
using DomainLayer.Errors;

namespace ApplicationLayer.Service
{
    public class AccountService : IAccountService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepo;
        private readonly ICrypterService _crypterService;
        private readonly IEmailService _emailService;
        private readonly ICaptchaVerificationService _captchaService;

        public AccountService(IJwtService jwtService, IUserRepository userRepo, ICrypterService crypterService, IEmailService emailService, ICaptchaVerificationService captchaService)
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
                return Response<LogInResponse>.Failure(AccountErrorHelper.InvalidCaptchaError());
            }
            var user = await _userRepo.GetByEmailAsync(request.Email);
            if (user == null)
            {
                return Response<LogInResponse>.Failure(AccountErrorHelper.InvalidCredentialsError());
            }

            if(user.Password == "00000000")
            {
                var token = _jwtService.GenerateResetPasswordToken(user.Id);

                try
                {
                    await _emailService.SendPasswordResetEmail(user.FirstName, user.Email, token);
                }
                catch (Exception)
                {
                    return Response<LogInResponse>.Failure(CommonErrorHelper.ServerError());
                }
                return Response<LogInResponse>.Failure(AccountErrorHelper.AccountNotActivatedError());

            }

            if (!_crypterService.CompareHash(request.Password, user.Password))
            {
                return Response<LogInResponse>.Failure(AccountErrorHelper.InvalidCredentialsError());
            }

            return Response<LogInResponse>.Success(new LogInResponse { Token = _jwtService.GenerateAccessToken(user.Id, user.FirstName, user.Email, user.Role) });
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
                return Response<MessageResponse>.Failure(AccountErrorHelper.ResetPasswordEmailSentFailureError());
            }

            return Response<MessageResponse>.Success(new MessageResponse());
        }

        public async Task<Response<MessageResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var tokenValidationResult = _jwtService.VerifyResetPasswordToken(request.Token);

            if(!tokenValidationResult.IsSuccess)
            {
                return Response<MessageResponse>.Failure(tokenValidationResult.ServiceError!);
            }

            var id = _jwtService.GetClaimValue<int?>(tokenValidationResult.Value!, ClaimTypes.NameIdentifier);
            if(id == null)
            {
               return Response<MessageResponse>.Failure(AccountErrorHelper.TokenInvalidError());
            }
            var newPasswordHashed = _crypterService.EncryptString(request.Password);
            await _userRepo.UpdateUserPasswordAsync((int)id!, newPasswordHashed);
            return Response<MessageResponse>.Success(new MessageResponse());
        }

        public async Task<Response<MessageResponse>> DeleteUser(int id)
        {
            await _userRepo.DeleteUser(id);
            return Response<MessageResponse>.Success(new()
            {
                Message = "User Deleted Successfuly"
            });
        }
    }
}
