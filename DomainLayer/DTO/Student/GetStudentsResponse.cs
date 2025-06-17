

namespace DomainLayer.DTO.Student

{
    public class GetStudentsResponse
    {
        public List<StudentResponseWithDegree> Students { get; set; } = [];
        
        public required int Page {  get; set; }

        public bool LastPage { get; set; } = false;

        public string SearchString { get; set; } = "";
    }

}
