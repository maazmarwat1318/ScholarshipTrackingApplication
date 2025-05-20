using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class Degree
    {
        public int Id { get; set; }
        public required string DegreeTitle { get; set; }

        public int? DepartmentId { get; set; }
    }
}
