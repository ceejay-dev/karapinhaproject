using Karapinha.Model;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IUsuarioService
    {
        Task<Usuario> CreateUser(Usuario usuario, IFormFile foto);
        Task<UsuarioDTO> GetUserById(int id);
        List<UsuarioDTO> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task<Usuario> UpdateUser(Usuario usuario, int id);
    }
}
