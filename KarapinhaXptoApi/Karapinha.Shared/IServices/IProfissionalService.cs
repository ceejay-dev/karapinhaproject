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
    public interface IProfissionalService
    {
        Task<ProfissionalDTO> CreateProfissional(ProfissionalDTO dto, IFormFile foto);
        Task<ProfissionalDTO> GetProfissionalById(int id);
        Task<IEnumerable<ProfissionalDTO>> GetAllProfissionals();
        Task<bool> DeleteProfissional(int id);
        IEnumerable<dynamic> GetAllProfissionaisByIdCategoria();
    }
}
