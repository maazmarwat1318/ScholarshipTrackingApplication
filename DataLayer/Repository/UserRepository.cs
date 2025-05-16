using Contracts.DataLayer;
using DataLayer.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace DataLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StaDbContext _dbContext;
        private readonly ILogger _logger;

        public UserRepository(StaDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddAsync(DomainLayer.Entity.User entity)
        {
            try
            {
                var dbEntity = UserMapper.MapDomainEntityToDb(entity);
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
                var dbEntity = UserMapper.MapDomainEntityToDb(entity);
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
                return dbEntities.Select(UserMapper.MapDbEntityToDomain);
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
                return dbEntity == null ? null : UserMapper.MapDbEntityToDomain(dbEntity);
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
                return dbEntity == null ? null : UserMapper.MapDbEntityToDomain(dbEntity);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(GetByEmailAsync)}");
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
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(SaveChangesAsync)}");
                throw;
            }

        }

        public async Task<bool> UpdateUserPasswordAsync(int id, string newPassword)
        {
            try
            {
                await _dbContext.Users.Where(e => e.Id == id).ExecuteUpdateAsync(e => e.SetProperty( e => e.Password, newPassword));
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(UpdateUserPasswordAsync)}");
                throw;
            }
        }
    }
}
