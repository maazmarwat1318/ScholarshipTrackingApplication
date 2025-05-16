using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Errors.AuthenticationErrors
{
    public static class AuthenticationErrorHelper
    {
        public static readonly string UserNotFoundCode = "UserNotFound";
        public static readonly string InvalidCredentialsCode = "InvalidCredentials";
        public static readonly string ResetPasswordEmailSentFailureCode = "ResetPasswordEmailSentFailure";
        public static readonly string TokenExpiredCode = "TokenExpired";
        public static readonly string TokenInvalidCode = "TokenInvalid";
        public static readonly string InvalidCaptchaCode = "InvalidCaptcha";

        public static ServiceError UserNotFoundError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = UserNotFoundCode, StatusCode = 404, Exception = e, Message = message ?? "User not found." };
        }

        public static ServiceError InvalidCredentialsError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = InvalidCredentialsCode, StatusCode = 409, Exception = e, Message = message ?? "Invalid Credentials." };
        }

        public static ServiceError ResetPasswordEmailSentFailureError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = ResetPasswordEmailSentFailureCode, StatusCode = 503, Exception = e, Message = message ?? "Failed to send a reset password email." };
        }

        public static ServiceError TokenExpiredError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = TokenExpiredCode, StatusCode = 498, Exception = e, Message = message ?? "Token Expired." };
        }

        public static ServiceError TokenInvalidError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = TokenInvalidCode, StatusCode = 498, Exception = e, Message = message ?? "Token Invalid." };
        }

        public static ServiceError InvalidCaptchaError(string? message = null, Exception? e = null)
        {
            return new ServiceError() { ErrorCode = InvalidCaptchaCode, StatusCode = 498, Exception = e, Message = message ?? "Failed to verify Captcha. Please try again" };
        }

    }
}
