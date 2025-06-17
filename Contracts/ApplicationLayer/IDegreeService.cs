using DomainLayer.Common;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IDegreeService
    {
        Task<Response<List<Degree>>> GetAllDegrees();
    }
}
