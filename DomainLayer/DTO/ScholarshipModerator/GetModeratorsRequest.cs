

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class GetModeratorsRequest
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; } = 10;
    }
}
