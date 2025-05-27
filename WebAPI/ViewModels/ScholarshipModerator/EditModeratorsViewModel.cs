using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DataLayer.Entity;
using DomainLayer.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAPI.ViewModels.ScholarshipModerator
{
    public class EditScholarshipModeratorViewModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last Name")]
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public Role Role { get; set; }

        [Required]
        public int ModeratorId;

        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            if (Role == Role.Student)
            {
                yield return new ValidationResult("The Student role is not allowed.", new[] { "Role" });
            }
        }


    }
}
