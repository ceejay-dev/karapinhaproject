using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Karapinha.Model
{
    [Table("utilizadores")]
    public class Utilizador
    {
        [Key]
        public int IdUtilizador { get; set; }
        public string? NomeUtilizador { get; set; }
        [Unique]
        public string? EmailUtilizador { get; set; }
        [MaxLength(9)]
        public string? TelemovelUtilizador { get; set; }
        [Unique]
        public string? BilheteUtilizador { get; set; }
        public string? FotoUtilizador { get; set; }
        [Unique]
        public string? UsernameUtilizador { get; set; }
        public string? PasswordUtilizador { get; set; }
        public string? Estado { get; set; }
        public string ? TipoPerfil { get; set; }

        public void EncriptarPassword(string password)
        {
            PasswordUtilizador = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerificarPassword(string password)
        {
            return (password == PasswordUtilizador);
            //return BCrypt.Net.BCrypt.Verify(password, PasswordUtilizador);
        }
    }
}
