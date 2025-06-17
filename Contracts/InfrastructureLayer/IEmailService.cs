

namespace Contracts.InfrastructureLayer
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string username, string useremail, string resetToken);
    }
}
