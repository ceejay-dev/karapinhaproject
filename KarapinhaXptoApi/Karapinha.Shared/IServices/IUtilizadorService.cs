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
    public interface IUtilizadorService
    {
        Task<UtilizadorDTO> CreateUser(UtilizadorDTO Utilizador, IFormFile foto);
        Task<UtilizadorDTO> GetUserById(int id);
        Task<IEnumerable<UtilizadorDTO>> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task UpdateUser(UtilizadorUpdateDTO Utilizador);
    }
}
