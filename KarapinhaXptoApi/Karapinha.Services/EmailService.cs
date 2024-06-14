using Karapinha.Shared.IEmail;
using Karapinnha.DTO;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSender emailSender;

        public EmailService(IOptions<EmailSender> emailSender)
        {
            this.emailSender = emailSender.Value;
        }
        public async Task SendEmailAdminOrCliente(string message, UtilizadorDTO utilizador)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailSender.SenderName, emailSender.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(utilizador.EmailUtilizador, utilizador.EmailUtilizador));
            emailMessage.Subject = "Criação de conta Administrativo";
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.CheckCertificateRevocation = false;
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(emailSender.SmtpServer, emailSender.SmtpPort, MailKit.Security.SecureSocketOptions.Auto);
                await client.AuthenticateAsync(emailSender.SmtpUser, emailSender.SmtpPass);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
