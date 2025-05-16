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
        public required string FirstName {  get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required Role Role { get; set; }

        public required string Token { get; set; }

    }
}
