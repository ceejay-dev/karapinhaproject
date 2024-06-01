using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class UtilizadorDTO
    {
        public int IdUtilizador { get; set; }
        public string? NomeUtilizador { get; set; }
        public string? EmailUtilizador { get; set; }
        public string? TelemovelUtilizador { get; set; }
        public string? FotoUtilizador { get; set; }
        public string? UsernameUtilizador { get; set; }
        public string? PasswordUtilizador { get; set; }
        public string? Estado { get; set; }
        public string? TipoConta { get; set; }
    }
}
