using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InfrastructureLayer
{
    public interface ICaptchaVerificationService
    {
        Task<bool> VerifyTokenAsync(string token);
    }
}
