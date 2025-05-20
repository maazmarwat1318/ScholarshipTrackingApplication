using DomainLayer.Common;
using DomainLayer.Entity;

namespace Contracts.DataLayer
{
    public interface IDegreeRepository
    {
        Task<List<DomainLayer.Entity.Degree>> GetAllDegrees();
    }
}
