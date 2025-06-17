using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IStudentService
    {
        Task<Response<MessageResponse>> AddStudent(Student student);
        Task<Response<GetStudentsResponse>> GetStudents(GetStudentsRequest request);

        Task<Response<GetStudentsResponse>> SearchStudentViaName(SearchStudentsViaNameRequest request);

        Task<Response<MessageResponse>> EditStudent(EditStudentRequest request);

        Task<Response<StudentResponseWithDegree>> GetStudentById(int id);

    }
}
