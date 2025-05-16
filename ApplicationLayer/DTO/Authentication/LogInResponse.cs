using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace ApplicationLayer.DTO.Authentication
{
    public class LogInResponse
    {
        public required string Token { get; set; }

    }
}
