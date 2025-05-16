using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO.Authentication
{
    public class LogInRequest
    {
        public required string Email { get;  set; }
        public required string Password { get; set; }

        public required string CaptchaToken { get; set; }
    }
}
