using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class SearchModeratorViaNameRequest : GetModeratorsRequest
    {
        public required string SearchString { get; set; }
    }
}
