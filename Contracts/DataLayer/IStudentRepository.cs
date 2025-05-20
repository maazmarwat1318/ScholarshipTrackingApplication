using DomainLayer.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.DataLayer
{
    public interface IStudentRepository
    {
        Task<Response<bool>> AddStudent(Student student);
        Task<GetStudentsResponse> GetStudents(GetStudentsRequest request);

    }
}
