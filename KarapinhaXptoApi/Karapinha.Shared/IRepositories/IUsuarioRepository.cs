using Karapinha.Model;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CreateUser(Usuario usuario, IFormFile foto);
        Task<Usuario> GetUserById(int id);
        List<Usuario> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task<Usuario> UpdateUser(Usuario usuario, int id);
    }
}
