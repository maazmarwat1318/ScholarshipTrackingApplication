using DomainLayer.DTO.Student;
using AutoMapper;
using Contracts.DataLayer;
using DomainLayer.Common;
using DomainLayer.Errors.AuthenticationErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
namespace DataLayer.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public StudentRepository(StaDbContext dbContext, ILogger<UserRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }



        public async Task<Response<bool>> AddStudent(DomainLayer.Entity.Student student)
        {
            try
            {
                var user = _mapper.Map<DataLayer.Entity.User>(student);
                var studentRecord = _mapper.Map<DataLayer.Entity.Student>(student);
                studentRecord.StudentNavigation = user;
                await _dbContext.Students.AddAsync(studentRecord);
                await SaveChangesAsync();
                return Response<bool>.Success(true);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
                {
                    return Response<bool>.Failure(AccountErrorHelper.UserAlreadyExistsError());
                }
                LogError(e, nameof(AddStudent));
                throw;

            }
            catch (Exception e)
            {
                LogError(e, nameof(AddStudent));
                throw;
            }
        }

        public async Task<GetStudentsResponse> GetStudents(GetStudentsRequest request)
        {
            try
            {
                var totalStudents = _dbContext.Students.Count();
                var students = await _dbContext.Students.Include(student => student.StudentNavigation).Include(student => student.Degree).Select(student =>
                        _mapper.Map<StudentResponseWithDegree>(new Tuple<DataLayer.Entity.Student, DataLayer.Entity.User, DataLayer.Entity.Degree?>(student, student.StudentNavigation, student.Degree))).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                return new GetStudentsResponse() { Page = request.Page, Students = students, LastPage = totalStudents == (((request.Page - 1) * request.PageSize) + students.Count) };
            }
            catch (Exception e)
            {
                LogError(e, nameof(GetStudents));
                throw;
            }
        }

        public async Task<StudentResponseWithDegree?> GetStudentById(int id)
        {
            try
            {
                var student = await _dbContext.Students.Include(student => student.StudentNavigation).Include(student => student.Degree).Where(student => student.Id == id).Select(student =>
                        _mapper.Map<StudentResponseWithDegree>(new Tuple<DataLayer.Entity.Student, DataLayer.Entity.User, DataLayer.Entity.Degree?>(student, student.StudentNavigation, student.Degree))).FirstOrDefaultAsync();
                return student;
            }
            catch (Exception e)
            {
                LogError(e, nameof(GetStudents));
                throw;
            }
        }

        public async Task<GetStudentsResponse> SearchStudentViaName(SearchStudentsViaNameRequest request)
        {
            try
            {
                var matchingStudents = _dbContext.Students.Where(student => student.StudentNavigation.FirstName.Contains(request.SearchString) || student.StudentNavigation.LastName.Contains(request.SearchString));
                var totalStudents = matchingStudents.Count();
                var students = await matchingStudents.Include(student => student.StudentNavigation).Include(student => student.Degree).Select(student =>
                        _mapper.Map<StudentResponseWithDegree>(new Tuple<DataLayer.Entity.Student, DataLayer.Entity.User, DataLayer.Entity.Degree?>(student, student.StudentNavigation, student.Degree))).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                return new GetStudentsResponse() { Page = request.Page, Students = students, LastPage = totalStudents == (((request.Page - 1) * request.PageSize) + students.Count) };
            }
            catch (Exception e)
            {
                LogError(e, nameof(SearchStudentViaName));
                throw;
            }
        }

        public async Task<Response<bool>> EditStudent(EditStudentRequest request)
        {
            try
            {
                await _dbContext.Users
                        .Where(u => u.Id == request.StudentId)
                        .ExecuteUpdateAsync(u => u
                         .SetProperty(user => user.FirstName, request.FirstName)
                         .SetProperty(user => user.LastName, request.LastName)
                         .SetProperty(user => user.Email, request.Email));

                await _dbContext.Students.Where(student => student.Id == request.StudentId).ExecuteUpdateAsync(
                    s => s.SetProperty(s => s.DegreeId, request.DegreeId
                    ));
                return Response<bool>.Success(true);
            }
            catch (Exception e)
            {
                LogError(e, nameof(EditStudent));
                throw;
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                LogError(e, nameof(SaveChangesAsync));
                throw;
            }

        }

        private void LogError(Exception e, string methodName)
        {
            _logger.LogError(e, $"Repository Error occured at {nameof(StudentRepository)} in {methodName}");
        }

    }
}
