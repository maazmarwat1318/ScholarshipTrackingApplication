using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IAccountService
    {
        Task<Response<LogInResponse>> Login( LogInRequest request);
        Task<Response<MessageResponse>> ForgotPassword(ForgotPasswordRequest request);

        Task<Response<MessageResponse>> ResetPassword(ResetPasswordRequest request);

        Task DeleteUser(int id);

        

    }
}
