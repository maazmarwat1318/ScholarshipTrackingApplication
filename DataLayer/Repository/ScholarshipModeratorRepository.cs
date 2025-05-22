using AutoMapper;
using Contracts.DataLayer;
using DataLayer.Entity;
using DomainLayer.Common;
using DomainLayer.DTO.ScholarshipModerator;
using DomainLayer.Entity;
using DomainLayer.Errors.AuthenticationErrors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
namespace DataLayer.Repository
{
    public class ScholarshipModeratorRepository : IScholarshipModeratorRepository
    {
        private readonly StaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ScholarshipModeratorRepository(StaDbContext dbContext, ILogger<ScholarshipModeratorRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<bool>> AddScholarshipModerator(DomainLayer.Entity.ScholarshipModerator moderator)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                var user = _mapper.Map<DataLayer.Entity.User>(moderator);
                await _dbContext.Users.AddAsync(user);
                await SaveChangesAsync();

                var moderatorRecord = _mapper.Map<DataLayer.Entity.ScholarshipModerator>(moderator);
                moderatorRecord.Id = user.Id;
                await _dbContext.ScholarshipModerators.AddAsync(moderatorRecord);
                await SaveChangesAsync();
                transaction.Commit();
                return Response<bool>.Success(true);
            }
            catch (DbUpdateException e)
            {
                transaction.Rollback();
                if (e.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
                {
                    return Response<bool>.Failure(AccountErrorHelper.UserAlreadyExistsError());
                }
                _logger.LogError(e, $"Repository Error occured at ${nameof(ScholarshipModeratorRepository)} in ${nameof(AddScholarshipModerator)}");
                throw;

            }
            catch (Exception e)
            {
                transaction.Rollback();
                _logger.LogError(e, $"Repository Error occured at ${nameof(ScholarshipModeratorRepository)} in ${nameof(AddScholarshipModerator)}");
                throw;
            }
        }

        public async Task<GetModeratorsResponse> GetModerators(GetModeratorsRequest request)
        {
            try
            {
                  var totalModerators =  _dbContext.ScholarshipModerators.Count();
                var moderators = await _dbContext.ScholarshipModerators.Include(student => student.Moderator).Include(student => student.Moderator).Select(student =>
                        _mapper.Map<ScholarshipModeratorResponse>(new Tuple<DataLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>(student, student.Moderator))).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                return new GetModeratorsResponse() { Page = request.Page, Moderators = moderators, LastPage = totalModerators == (((request.Page - 1) * request.PageSize) + moderators.Count) };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(ScholarshipModeratorRepository)} in ${nameof(GetModerators)}");
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
                _logger.LogError(e, $"Repository Error occured at ${nameof(ScholarshipModeratorRepository)} in ${nameof(SaveChangesAsync)}");
                throw;
            }

        }

        public async Task<GetModeratorsResponse> SearchModeratosViaName(SearchModeratorViaNameRequest request)
        {
            try
            {
                var matchingModerators = _dbContext.ScholarshipModerators.Where(moderator => moderator.Moderator.FirstName.Contains(request.SearchString) || moderator.Moderator.LastName.Contains(request.SearchString));
                var totalModerators = matchingModerators.Count();
                var moderators = await matchingModerators.Include(moderator => moderator.Moderator).Select(moderator =>
                        _mapper.Map<ScholarshipModeratorResponse>(new Tuple<DataLayer.Entity.ScholarshipModerator, DataLayer.Entity.User>(moderator, moderator.Moderator))).Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToListAsync();
                return new GetModeratorsResponse() { Page = request.Page, Moderators = moderators, LastPage = totalModerators == (((request.Page - 1) * request.PageSize) + moderators.Count) };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at {nameof(ScholarshipModeratorRepository)} in {nameof(SearchModeratosViaName)}");
                throw;
            }
        }

    }
}
