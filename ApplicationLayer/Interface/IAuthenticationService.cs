using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using ApplicationLayer.DTO.Authentication;
using ApplicationLayer.DTO.Common;

namespace ApplicationLayer.Interface
{
    public interface IAuthenticationService
    {
        Task<Response<LogInResponse>> Login( LogInRequest request);
        Task<Response<MessageResponse>> ForgotPassword(ForgotPasswordRequest request);

        Task<Response<MessageResponse>> ResetPassword(ResetPasswordRequest request);
    }
}
