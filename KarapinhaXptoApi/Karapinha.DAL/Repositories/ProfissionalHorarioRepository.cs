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
    public class ProfissionalHorarioRepository : IProfissionalHorarioRepository
    {
        private readonly KarapinhaDbContext dbContext;

        public ProfissionalHorarioRepository(KarapinhaDbContext context)
        {
            dbContext = context;
        }

        public async Task<ProfissionalHorario> CreateProfissionalHorario(ProfissionalHorario profissionalHorario)
        {
            var profHorario = await dbContext.AddAsync(profissionalHorario);
            await dbContext.SaveChangesAsync();
            return profHorario.Entity;
        }

        public async Task<ProfissionalHorario> GetProfissionalById(int id)
        {
            return await dbContext.ProfissionalHorarios
             .Where(u => u.IdProfissional == id)
             .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProfissionalHorario(int id)
        {
            var profissional = await GetProfissionalById(id);

            if (profissional != null)
            {
                dbContext.ProfissionalHorarios.Remove(profissional);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
