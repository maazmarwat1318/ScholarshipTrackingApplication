using DomainLayer.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.DataLayer
{
    public interface IStudentRepository
    {
        Task<Response<bool>> AddStudent(Student student);
        Task<GetStudentsResponse> GetStudents(GetStudentsRequest request);

        Task<GetStudentsResponse> SearchStudentViaName(SearchStudentsViaNameRequest request);

        Task<Response<bool>> EditStudent(EditStudentRequest request);

        Task<StudentResponse?> GetStudentById(int id);

    }
}
