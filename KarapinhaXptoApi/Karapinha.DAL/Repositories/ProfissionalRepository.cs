using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class ProfissionalRepository : IProfissionalRepository
    {
        private readonly KarapinhaDbContext DbContext;
        public ProfissionalRepository(KarapinhaDbContext context)
        {
            DbContext = context;
        }

        public async Task<Profissional> CreateProfissional(Profissional profissional,IFormFile foto)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            string fileName = Path.GetFileName(foto.FileName);
            string filePath = Path.Combine(imagePath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(fileStream);
            }
            profissional.FotoProfissional = fileName;

            var employee = await DbContext.AddAsync(profissional);
            await DbContext.SaveChangesAsync();
            return employee.Entity;
        }

        public async Task<Profissional> GetProfissionalById(int id)
        {
            return await DbContext.Profissionais.FindAsync(id);
        }

        public async Task<IEnumerable<Profissional>> GetAllProfissionals()
        {
            return await DbContext.Profissionais.Include(p=>p.Horarios).ThenInclude(p=>p.Horario).ToListAsync();
        }

        public async Task<bool> DeleteProfissional(int id)
        {
            var profissional = await GetProfissionalById(id);

            if (profissional != null)
            {
                DbContext.Profissionais.Remove(profissional);
                await DbContext.SaveChangesAsync();
                return true;
            } else { return false; }
        }

        public async Task UpdateProfissional(Profissional profissional)
        {
             DbContext.Profissionais.Update(profissional);
             await DbContext.SaveChangesAsync();
        }

        public IEnumerable<dynamic> GetAllProfissionaisByIdCategoria()
        {
            var result = from profissional in DbContext.Profissionais
                         join categoria in DbContext.Categorias
                         on profissional.FkCategoria equals categoria.IdCategoria
                         select new
                         {
                             NomeProfissional = profissional.NomeProfissional,
                             EmailProfissional = profissional.EmailProfissional,
                             NomeCategoria = categoria.NomeCategoria,
                             TelemovelProfissional = profissional.TelemovelProfissional
                         };

            return result.ToList();
        }
    }
}
