using Karapinha.Model;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface ICategoriaRepository
    {
        Task <Categoria> CreateCategory (Categoria categoria);
        Task<Categoria> GetCategoryById(int id);
        Task<List<Categoria>> GetAllCategories();
        Task<bool> DeleteCategory(int id);
        Task UpdateCategory(Categoria categoria);
    }
}
