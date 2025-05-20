using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.DTO.Degree;
using DomainLayer.Common;
using DomainLayer.Entity;

namespace Contracts.ApplicationLayer.Interface
{
    public interface IDegreeService
    {
        Task<Response<List<Degree>>> GetAllDegrees();
    }
}
