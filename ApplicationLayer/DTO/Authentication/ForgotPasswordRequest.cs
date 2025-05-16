using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO.Authentication
{
    public class ForgotPasswordRequest
    {
        public required string Email { get; set; }

    }
}
