using Karapinha.Model;
using Karapinha.Shared.IRepositories;
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

        public async Task <ProfissionalHorario> CreateProfissionalHorario (ProfissionalHorario profissionalHorario)
        {
            var profHorario = await dbContext.AddAsync(profissionalHorario);
            await dbContext.SaveChangesAsync();
            return profHorario.Entity;
        }
    }
}
