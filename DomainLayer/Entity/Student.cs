using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public sealed class Student : User
    {
        public int? DegreeId { get; set; }
    }
}
