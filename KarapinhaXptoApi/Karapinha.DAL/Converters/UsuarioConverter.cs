using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Converters
{
    public abstract class UsuarioConverter
    {
        public static Usuario ToUsuario(UsuarioDTO usuario) {
            var user = new Usuario();
            user.NomeUsuario = usuario.NomeUsuario;
            user.EmailUsuario = usuario.EmailUsuario;
            user.TelemovelUsuario = usuario.TelemovelUsuario;
            user.FotoUsuario = usuario.FotoUsuario;
            user.UsernameUsuario = usuario.UsernameUsuario;
            user.PasswordUsuario = usuario.PasswordUsuario;
            user.Estado = usuario.Estado;
            user.TipoConta = usuario.TipoConta;

            return user;
        }

        public static UsuarioDTO ToUsuarioDTO(Usuario usuario)
        {
            var user = new UsuarioDTO();
            user.NomeUsuario = usuario.NomeUsuario;
            user.EmailUsuario = usuario.EmailUsuario;
            user.TelemovelUsuario = usuario.TelemovelUsuario;
            user.FotoUsuario = usuario.FotoUsuario;
            user.UsernameUsuario = usuario.UsernameUsuario;
            user.PasswordUsuario = usuario.PasswordUsuario;
            user.Estado = usuario.Estado;
            user.TipoConta = usuario.TipoConta;

            return user;
        }
        
    }
}
