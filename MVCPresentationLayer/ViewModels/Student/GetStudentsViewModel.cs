using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.Student;

namespace MVCPresentationLayer.ViewModels.Student
{
    public class GetStudentsViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        public List<StudentResponse> Students { get; set; } = [];
    }
}
