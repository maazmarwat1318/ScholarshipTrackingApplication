

namespace Contracts.InfrastructureLayer
{
    public interface ICaptchaVerificationService
    {
        Task<bool> VerifyTokenAsync(string token);
    }
}
