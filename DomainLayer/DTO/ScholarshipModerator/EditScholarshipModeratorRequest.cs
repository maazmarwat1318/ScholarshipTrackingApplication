
using DomainLayer.Enums;

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class EditScholarshipModeratorRequest
    {
        public required int ModeratorId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required Role Role { get; set; }
    }
}
