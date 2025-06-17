

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class SearchModeratorViaNameRequest : GetModeratorsRequest
    {
        public required string SearchString { get; set; }
    }
}
