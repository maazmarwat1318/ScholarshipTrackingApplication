using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Errors
{
    public class ErrorResponse
    {
        public required string ErrorCode { get; set; }
        public string? Message { get; set; }
        public required int StatusCode { get; set; }
    }
}
