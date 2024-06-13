using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MimeKit;
using Microsoft.Extensions.Logging;
using Karapinha.Shared.IServices;
using Karapinha.Model;
using Karapinha.Shared.IEmail;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Karapinha.Services
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string toEmail, string subject)
        {
            // Set up SMTP client
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("20200054@isptec.co.ao", "Angola12345");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("20200054@isptec.co.ao");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Novo Utilizador Registado</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Um novo utilizador com endereço de email </p>", toEmail, "<p> se registou no Karapinha Dura XPTO, e precisa que o administrador active a sua conta.</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
