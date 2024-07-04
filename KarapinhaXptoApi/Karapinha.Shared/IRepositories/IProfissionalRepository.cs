using Karapinha.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IProfissionalRepository
    {
        Task <Profissional> CreateProfissional (Profissional profissional, IFormFile foto);
        Task<Profissional> GetProfissionalById(int id);
        Task<Profissional> GetProfissionalByIdCategoria(int id);
        Task<IEnumerable<Profissional>> GetAllProfissionals();
        Task<bool> DeleteProfissional(int id);
        Task UpdateProfissional(Profissional profissional);
        IEnumerable<dynamic> GetAllProfissionaisWithCategoria();
        Task<IEnumerable<dynamic>> GetAllProfissionaisByIdCategoria(int idCategoria);
    }
}
