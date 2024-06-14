using Karapinha.Shared.IEmail;
using Karapinha.Shared.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karapinha.Model;
using Karapinnha.DTO;

namespace Karapinha.Services
{
    public class EmailSender
    {
        public string? SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string? SmtpUser { get; set; }
        public string? SmtpPass { get; set; }
        public string? SenderName { get; set; }
        public string? SenderEmail { get; set; }
    }
}
