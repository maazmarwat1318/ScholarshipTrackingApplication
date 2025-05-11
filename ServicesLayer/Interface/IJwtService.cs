
using DomainLayer.Enums;

namespace InfrastructureLayer.Interface
{
    internal interface IJwtService
    {
        string GenerateToken(string firstName, string lastName, string id, string email, Role role);
    }
}
