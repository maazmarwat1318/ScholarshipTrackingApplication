using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.DataLayer;
using DomainLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer.Repository
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly StaDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DegreeRepository(StaDbContext dbContext, ILogger<UserRepository> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<DomainLayer.Entity.Degree>> GetAllDegrees()
        {
            try
            {
                var degrees = await _dbContext.Degrees.ToListAsync();
                return _mapper.Map<List<DomainLayer.Entity.Degree>>(degrees);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Repository Error occured at ${nameof(DegreeRepository)} in ${nameof(GetAllDegrees)}");
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
                _logger.LogError(e, $"Repository Error occured at ${nameof(UserRepository)} in ${nameof(SaveChangesAsync)}");
                throw;
            }

        }
    }
}
