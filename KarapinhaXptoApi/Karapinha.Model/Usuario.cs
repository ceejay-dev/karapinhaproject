using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Model
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string? NomeUsuario { get; set; }
        [Unique]
        public string? EmailUsuario { get; set; }

        [MaxLength(9)]
        public string? TelemovelUsuario { get; set; }
        public string? FotoUsuario { get; set; }
        [Unique]
        public string? UsernameUsuario { get; set; }
        public string? PasswordUsuario { get; set; }
        public string? Estado { get; set; }
        public string? TipoConta { get; set; }
    }
}
