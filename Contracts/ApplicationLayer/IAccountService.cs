using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;


namespace Contracts.ApplicationLayer.Interface
{
    public interface IAccountService
    {
        Task<Response<LogInResponse>> Login( LogInRequest request);
        Task<Response<MessageResponse>> ForgotPassword(ForgotPasswordRequest request);

        Task<Response<MessageResponse>> ResetPassword(ResetPasswordRequest request);

        Task<Response<MessageResponse>> DeleteUser(int id);

        

    }
}
