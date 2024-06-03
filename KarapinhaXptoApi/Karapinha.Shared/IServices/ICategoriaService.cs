using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface ICategoriaService
    {
        Task<CategoriaDTO> CreateCategory(CategoriaDTO categoria);
        Task<CategoriaDTO> GetCategoryById(int id);
        Task<IEnumerable<CategoriaDTO>> GetAllCategories();
        Task<bool> DeleteCategory(int id);
        Task UpdateCategory(CategoriaDTO categoria);
    }
}
