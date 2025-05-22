using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTO.Student
{
    public class SearchStudentsViaNameRequest : GetStudentsRequest
    {
        public required string SearchString { get; set; }
    }
}
