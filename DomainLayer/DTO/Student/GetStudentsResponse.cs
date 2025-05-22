using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entity;

namespace DomainLayer.DTO.Student

{
    public class GetStudentsResponse
    {
        public List<StudentResponseWithDegree> Students = [];
        
        public required int Page {  get; set; }

        public bool LastPage { get; set; } = false;

        public string SearchString { get; set; } = "";
    }

}
