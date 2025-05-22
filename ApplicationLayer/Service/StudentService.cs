using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using Contracts.InfrastructureLayer;
using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;

namespace ApplicationLayer.Service
{
    public class StudentService : IStudentService
    {


        private readonly IStudentRepository _studentRepo;


        public StudentService( IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public async Task<Response<MessageResponse>> AddStudent(Student student)
        {
            var result = await _studentRepo.AddStudent(student);
            if (!result.IsSuccess)
            {
                return Response<MessageResponse>.Failure(result.ServiceError!);
            }
            return Response<MessageResponse>.Success(new()
            {
                Message = "Student created successfully"
            });

        }

        public async Task<GetStudentsResponse> GetStudents(GetStudentsRequest request)
        {
            var result = await _studentRepo.GetStudents(request);
            return result;

        }

        public async Task<GetStudentsResponse> SearchStudentViaName(SearchStudentsViaNameRequest request)
        {
            var result = await _studentRepo.SearchStudentViaName(request);
            return result;
        }


    }
}
