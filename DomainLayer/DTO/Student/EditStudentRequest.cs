

namespace DomainLayer.DTO.Student
{
    public class EditStudentRequest
    {
        public required int StudentId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required int DegreeId { get; set; }
    }
}
