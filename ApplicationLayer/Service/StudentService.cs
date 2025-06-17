using Contracts.ApplicationLayer.Interface;
using Contracts.DataLayer;
using DomainLayer.Common;
using DomainLayer.DTO.Common;
using DomainLayer.DTO.Student;
using DomainLayer.Entity;
using DomainLayer.Errors.AuthenticationErrors;

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

        public async Task<Response<MessageResponse>> EditStudent(EditStudentRequest request)
        {
            var result = await _studentRepo.EditStudent(request);
            return Response<MessageResponse>.Success(new()
            {
                Message = "Student Updated Successfuly"
            });
        }

        public async Task<Response<StudentResponseWithDegree>> GetStudentById(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if(student == null)
            {
                return Response<StudentResponseWithDegree>.Failure(AccountErrorHelper.UserNotFoundError());
            }
            return Response<StudentResponseWithDegree>.Success(student);
        }

        public async Task<Response<GetStudentsResponse>> GetStudents(GetStudentsRequest request)
        {
            var result = await _studentRepo.GetStudents(request);
            return Response<GetStudentsResponse>.Success(result);

        }

        public async Task<Response<GetStudentsResponse>> SearchStudentViaName(SearchStudentsViaNameRequest request)
        {
            var result = await _studentRepo.SearchStudentViaName(request);
            return Response<GetStudentsResponse>.Success(result);
        }
    }
}
