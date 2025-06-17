using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.ViewModels.Student
{
    public class CreateStudentViewModel
    {

        [Required(AllowEmptyStrings = false)]
        [DisplayName("First Name")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Last Name")]
        [Required(AllowEmptyStrings = false)]
        public  string LastName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public  string Email { get; set; } = null!;

        [Required]
        [DisplayName("Degree")]
        [Range(0, int.MaxValue)]
        public int DegreeId { get; set; }


    }
}
