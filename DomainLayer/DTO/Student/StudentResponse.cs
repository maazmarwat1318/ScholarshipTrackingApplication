
using DomainLayer.DTO.Account;

namespace DomainLayer.DTO.Student
{
    public class StudentResponse : UserResponse
    {
        public int? DegreeId { get; set; }

    }
}
