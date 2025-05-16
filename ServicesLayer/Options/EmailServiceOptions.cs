using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Options
{
    public class EmailServiceOptions
    {
        public string Host { get; set; } = null!;
        public string ApiToken { get; set; } = null!;

        public string ResetPasswordUrl { get; set; } = null!;
    }
}
