using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class UtilizadorUpdateDTO
    {
        public int IdUtilizador { get; set; }
        public string? NomeUtilizador { get; set; }
        public string? EmailUtilizador { get; set; }
        public string? BilheteUtilizador { get; set; }
        public string? TelemovelUtilizador { get; set; }
        //public string? FotoUtilizador { get; set; }
        public string? UsernameUtilizador { get; set; }
        public string? PasswordUtilizador { get; set; }
    }
}
