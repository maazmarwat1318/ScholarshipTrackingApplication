using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity;

namespace DomainLayer.DTO.ScholarshipModerator
{
    public class GetModeratorsResponse
    {
        public List<ScholarshipModeratorResponse> Moderators { get; set; } = [];

        public required int Page { get; set; }

        public bool LastPage { get; set; } = false;

        public string SearchString { get; set; } = "";
    }

}
