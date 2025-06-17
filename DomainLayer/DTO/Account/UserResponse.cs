
using DomainLayer.Enums;

namespace DomainLayer.DTO.Account
{
    public class UserResponse
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required Role Role { get; set; }
    }
}
