using Karapinha.Model;
using Karapinnha.DTO.Profissional;
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
        Task<ProfissionalDTO> GetProfissionalByIdCategoria(int id);
        Task<IEnumerable<ProfissionalDTO>> GetAllProfissionals();
        Task<bool> DeleteProfissional(int id);
        IEnumerable<dynamic> GetAllProfissionaisWithCategoria();
        Task<IEnumerable<dynamic>> GetAllProfissionaisByIdCategoria(int idCategoria);
        Task<IEnumerable<ProfissionalDTO>> GetMostRequestedProfessionals();
    }
}
