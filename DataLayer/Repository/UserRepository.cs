using AutoMapper;
using Contracts.DataLayer;
using DomainLayer.Common;
using DomainLayer.Errors.AuthenticationErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
namespace DataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserRepository(StaDbContext dbContext, ILogger<UserRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task AddAsync(DomainLayer.Entity.User entity)
        {
            try
            {
                var dbEntity = _mapper.Map<DataLayer.Entity.User>(entity);
                await _dbContext.Users.AddAsync(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(AddAsync)}");
                throw;
            }
        }

        public void Delete(DomainLayer.Entity.User entity)
        {
            try
            {
                var dbEntity = _mapper.Map<DataLayer.Entity.User>(entity);
                _dbContext.Users.Remove(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(Delete)}");
                throw;
            }
        }

        public async Task<IEnumerable<DomainLayer.Entity.User>> GetAllAsync()
        {
            try
            {
                var dbEntities = await _dbContext.Users.ToListAsync();
                return dbEntities.Select(_mapper.Map<DomainLayer.Entity.User>);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(GetAllAsync)}");
                throw;
            }
        }

        public async Task<DomainLayer.Entity.User?> GetByIdAsync(int id)
        {
            try
            {
                var dbEntity = await _dbContext.Users.FirstOrDefaultAsync(e => e.Id == id);
                return dbEntity == null ? null : _mapper.Map<DomainLayer.Entity.User>(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(GetByIdAsync)}");
                throw;
            }
        }

        public async Task<DomainLayer.Entity.User?> GetByEmailAsync(string email)
        {
            try
            {
                var dbEntity = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
                return dbEntity == null ? null : _mapper.Map<DomainLayer.Entity.User>(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(GetByEmailAsync)}");
                throw;
            }
        }

        

        public async Task<bool> UpdateUserPasswordAsync(int id, string newPassword)
        {
            try
            {
                await _dbContext.Users.Where(e => e.Id == id).ExecuteUpdateAsync(e => e.SetProperty(e => e.Password, newPassword));
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(UpdateUserPasswordAsync)}");
                throw;
            }
        }

        public async Task<Response<bool>> AddStudent(DomainLayer.Entity.Student student)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var user = _mapper.Map<DataLayer.Entity.User>(student);
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                var studentRecord = _mapper.Map<DataLayer.Entity.Student>(student);
                studentRecord.Id = user.Id;
                await _dbContext.Students.AddAsync(studentRecord);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                return Response<bool>.Success(true);
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                if (e.Number == 1062)
                {
                    return Response<bool>.Failure(AccountErrorHelper.UserAlreadyExistsError());
                }
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(AddStudent)}");
                throw;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(AddStudent)}");
                throw;
            }
        }

        public async Task<Response<bool>> AddScholarshipModerator(DomainLayer.Entity.ScholarshipModerator moderator)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var user = _mapper.Map<DataLayer.Entity.User>(moderator);
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                var moderatorRecord = _mapper.Map<DataLayer.Entity.ScholarshipModerator>(moderator);
                moderatorRecord.Id = user.Id;
                await _dbContext.ScholarshipModerators.AddAsync(moderatorRecord);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
                return Response<bool>.Success(true);
            }
            catch (MySqlException e)
            {
                transaction.Rollback();
                if (e.Number == 1062)
                {
                    return Response<bool>.Failure(AccountErrorHelper.UserAlreadyExistsError());
                }
                _logger.LogError(e, $"Repository Error occured at {nameof(UserRepository)} in {nameof(AddStudent)}");
                throw;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                _logger.LogError(e, $"Repository Error occured at {nameof(UserRepository)} in {nameof(AddStudent)}");
                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at {nameof(UserRepository)} in {nameof(SaveChangesAsync)}");
                throw;
            }

        }

        public async Task DeleteUser(int id)
        {
            try
            {
                await _dbContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at {nameof(UserRepository)} in {nameof(DeleteUser)}");
                throw;
            }
        }

    }
}
