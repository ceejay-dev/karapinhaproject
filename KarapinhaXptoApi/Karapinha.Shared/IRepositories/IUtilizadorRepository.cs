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
    public interface IUtilizadorRepository
    {
        Task<Utilizador> CreateUser(Utilizador Utilizador, IFormFile foto);
        Task<Utilizador> GetUserById(int id);
        Task<List<Utilizador>> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task UpdateUser(Utilizador Utilizador);
    }
}
