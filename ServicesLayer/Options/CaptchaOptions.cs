using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Options
{
    public class CaptchaOptions
    {
        public string ClientKey { get; set; } = null!;
        public string ServerKey { get; set; } = null!;
    }
}
