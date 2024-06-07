using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly KarapinhaDbContext dbContext;

        public CategoriaRepository(KarapinhaDbContext context)
        {
            dbContext = context;
        }

        public async Task<Categoria> CreateCategory(Categoria categoria)
        {
            var category = await dbContext.AddAsync(categoria);
            
            await dbContext.SaveChangesAsync();
            return category.Entity;
        }

        public async Task<Categoria> GetCategoryById(int id)
        {
            return await dbContext.Categorias.FindAsync(id);
        }

        public async Task<List<Categoria>> GetAllCategories()
        {
            return await dbContext.Categorias.ToListAsync();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);

            if (category != null) {
                 dbContext.Categorias.Remove(category);
                 await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateCategory(Categoria categoria)
        {
            dbContext.Categorias.Update(categoria);
            await dbContext.SaveChangesAsync();
        }
    }
}
