using Microsoft.Extensions.Options;
using System.Threading.Tasks;
//using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using Microsoft.Extensions.Logging;
using Karapinha.Shared.IServices;
using Karapinha.Model;
using Karapinha.Shared.IEmail;
using System.Net.Mail;
using System.Net;
using System.Text;
using Karapinha.Shared.IRepositories;

namespace Karapinha.Services
{
    public class EmailReceiver : IEmailReceiver
    {
        public void SendEmailAdmin(string toEmail)
        {
            //Definindo o SMTP Cliente
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("candidojoao12@gmail.com", "934818736");

            // Criando a mensagem de email
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("candidojoao12@gmail.com");
            mailMessage.To.Add(toEmail);
            mailMessage.Subject = "NOVO CLIENTE REGISTADO.";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h4>Um novo cliente",toEmail,"se registou no Karapinha Dura XPTO, e precisa que administrador active a sua conta para que tenha acesso aos serviços do salão.</h4>");
            mailBody.AppendFormat("<br/>");
            mailBody.AppendFormat("<p>Karapinha Dura XPTO</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
