using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.Student;

namespace WebAPI.ViewModels.Student
{
    public class GetStudentsViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        public int PageSize { get; set; } = 10;

        public string SearchString { get; set; } = "";

    }
}
