using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinnha.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string? NomeUsuario { get; set; }
        public string? EmailUsuario { get; set; }
        public string? TelemovelUsuario { get; set; }
        public string? FotoUsuario { get; set; }
        public string? UsernameUsuario { get; set; }
        public string? PasswordUsuario { get; set; }
        public string? Estado { get; set; }
        public string? TipoConta { get; set; }
    }
}
