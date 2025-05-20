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
        public List<StudentResponse> Students = [];
        
        public required int Page {  get; set; }
    }

}
