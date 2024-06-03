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
        Task<UtilizadorCreateDTO> CreateUser(UtilizadorCreateDTO Utilizador, IFormFile foto);
        Task<UtilizadorCreateDTO> GetUserById(int id);
        Task<IEnumerable<UtilizadorCreateDTO>> GetAllUsers();
        Task<bool> DeleteUser(int id);
        Task UpdateUser(UtilizadorUpdateDTO Utilizador);
    }
}
