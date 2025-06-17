
using DomainLayer.Enums;

namespace DataLayer.Entity;

public partial class User
{
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; } 

    public required string Email { get; set; }

    public required Role Role { get; set; }

    public required string Password { get; set; }

    public virtual ScholarshipModerator? ScholarshipModerator { get; set; }

    public virtual Student? Student { get; set; }
}
