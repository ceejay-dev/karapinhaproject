using Karapinnha.DTO.Utilizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IEmail
{
    public interface IEmailService
    {
        Task SendEmailAdminOrCliente(string message, string subject,UtilizadorDTO utilizador);
    }
}
