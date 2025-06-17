
namespace DomainLayer.DTO.Student
{
    public class SearchStudentsViaNameRequest : GetStudentsRequest
    {
        public required string SearchString { get; set; }
    }
}
