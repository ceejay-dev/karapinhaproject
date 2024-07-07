using Karapinha.Model;
using Karapinnha.DTO.Utilizador;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IUtilizadorService
    {
        Task<UtilizadorDTO> CreateUser(UtilizadorDTO Utilizador, IFormFile foto);
        Task<UtilizadorDTO> GetUserById(int id);
        Task<IEnumerable<UtilizadorDTO>> GetAllUsers();
        Task<IEnumerable<UtilizadorDTO>> GetAllClientes();
        Task<IEnumerable<UtilizadorDTO>> GetAllAdministratives();
        Task<bool> DeleteUser(int id);
        Task UpdateUser(UtilizadorUpdateDTO Utilizador);
        Task ActivateOrDesactivateClient(int cliente);
        Task<UtilizadorDTO> Login(LoginDTO login);
        Task<string> GetUserRole(string username);
        Task<bool> VerifyAdministrativeStatus(UtilizadorDTO dto);
        Task ActivateAndChangePassword(string username, string password);
        Task<int> GetUserIdByUsername(string username);
        Task<UtilizadorDTO> GetUserByUsername(string username);
    }
}
