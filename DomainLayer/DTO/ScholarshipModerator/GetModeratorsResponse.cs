

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class GetModeratorsResponse
    {
        public List<ScholarshipModeratorResponse> Moderators { get; set; } = [];

        public required int Page { get; set; }

        public bool LastPage { get; set; } = false;

        public string SearchString { get; set; } = "";
    }

}
