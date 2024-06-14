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
            // Definindo o SMTP Cliente
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("candidojoao12@gmail.com", "hkck izvj jrns zrmk")
            };

            // Criando a mensagem de email
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("candidojoao12@gmail.com"),
                Subject = "O SEU REGISTO FOI REALIZADO COM SUCESSO.",
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h4>Seja bem vindo à Karapinha Dura XPTO, a sua conta será activada pelo administrador para que tenhas acesso aos serviços do salão.</h4>", toEmail);
            mailBody.Append("<br/>");
            mailBody.Append("<p>Karapinha Dura XPTO -- Baixera Sempre Vence!</p>");
            mailMessage.Body = mailBody.ToString();

            // Enviar email
            client.Send(mailMessage);
        }

    }
}
