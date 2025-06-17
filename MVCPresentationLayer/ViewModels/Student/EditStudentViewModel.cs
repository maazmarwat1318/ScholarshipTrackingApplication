using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCPresentationLayer.ViewModels.Student
{
    public class EditStudentViewModel
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
        [DisplayName("Degree")]
        [Range(0, int.MaxValue)]
        public int DegreeId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public List<SelectListItem> Degrees { get; set; } = [];

    }
}
