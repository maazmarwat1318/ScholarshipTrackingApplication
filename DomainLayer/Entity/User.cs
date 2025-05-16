using DomainLayer.Enums;

namespace DomainLayer.Entity
{
    public class User
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required Role Role { get; set; }
    }
}
