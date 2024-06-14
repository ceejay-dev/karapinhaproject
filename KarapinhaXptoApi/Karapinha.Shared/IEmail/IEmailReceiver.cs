using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IEmail
{
    public interface IEmailReceiver
    {
        void SendEmailAdmin(string toEmail);
    }
}
