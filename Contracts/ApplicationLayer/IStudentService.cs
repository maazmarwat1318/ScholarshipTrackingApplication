using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Common;
using DomainLayer.DTO.Authentication;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IStudentService
    {
        Task<Response<MessageResponse>> AddStudent(Student student);
        Task<GetStudentsResponse> GetStudents(GetStudentsRequest request);

        Task<GetStudentsResponse> SearchStudentViaName(SearchStudentsViaNameRequest request);

        Task<Response<MessageResponse>> EditStudent(EditStudentRequest request);

        Task<Response<StudentResponse>> GetStudentById(int id);
    }
}
