using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.Student;

namespace MVCPresentationLayer.ViewModels.Student
{
    public class GetStudentsViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        public bool LastPage { get; set; } = false;

        public int PageSize { get; set; } = 10;

        public string SearchString { get; set; } = "";

        public List<StudentResponseWithDegree> Students { get; set; } = [];
    }
}
