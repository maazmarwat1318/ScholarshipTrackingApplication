using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class GetModeratorsRequest
    {
        public required int Page { get; set; }
        public required int PageSize { get; set; } = 10;
    }
}
