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
        Task<List<Utilizador>> GetAllClientes();
        Task<List<Utilizador>> GetAllAdministratives();
        Task<bool> DeleteUser(int id);
        Task UpdateUser(Utilizador Utilizador);
        Task<Utilizador> GetUserByUsername(string username);
        bool VerifyStatus(Utilizador utilizador);
        Task<string> GetUserRole(string username);
        Task<bool> VerifyAdministrativeStatus(Utilizador user);
        Task<int> GetUserIdByUsername(string username);
    }
}
