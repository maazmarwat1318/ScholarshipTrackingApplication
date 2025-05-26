using System.ComponentModel.DataAnnotations;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.DTO.Student;

namespace MVCPresentationLayer.ViewModels.ScholarshipModerator
{
    public class GetModeratorsViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }

        public bool LastPage { get; set; } = false;

        public int PageSize { get; set; } = 10;

        public string SearchString { get; set; } = "";

        public List<ScholarshipModeratorResponse> Moderators { get; set; } = [];
    }
}
