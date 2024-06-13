using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IEmailService
    {
        Task SendActivationEmail(string toEmail, string subject, string message);
    }
}
