using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.Degree
{
    public class GetDegreesResponseDto
    {
        public List<DomainLayer.Entity.Degree> Degrees { get; set; } = [];
    }
}
