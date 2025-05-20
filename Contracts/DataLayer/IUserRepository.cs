using DomainLayer.Common;
using DomainLayer.Entity;

namespace Contracts.DataLayer
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(int id);
        Task<bool> UpdateUserPasswordAsync(int id, string newPassword);

        Task DeleteUser(int id);
    }
}
