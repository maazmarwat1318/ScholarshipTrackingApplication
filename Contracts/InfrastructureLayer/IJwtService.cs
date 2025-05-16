using DomainLayer.Common;
using System.Security.Claims;
using DomainLayer.Enums;

namespace Contracts.InfrastructureLayer
{
    public interface IJwtService
    {
        string GenerateAccessToken(int id, string firstName, string email, Role role);
        string GenerateResetPasswordToken(int id);

        Response<ClaimsPrincipal> VerifyResetPasswordToken(string token);

        T? GetClaimValue<T>(ClaimsPrincipal principal, string claimKey);
    }
}
