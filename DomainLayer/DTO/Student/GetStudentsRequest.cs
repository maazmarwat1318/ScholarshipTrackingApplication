
namespace DomainLayer.DTO.Student
{
    public class GetStudentsRequest
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; } = 10;
    }
}
