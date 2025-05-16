using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.InfrastructureLayer
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string username, string useremail, string resetToken);
    }
}
