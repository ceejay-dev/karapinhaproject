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
    public class MarcacaoServicosRepository : IMarcacaoServicosRepository
    {
        private readonly KarapinhaDbContext Context;

        public MarcacaoServicosRepository(KarapinhaDbContext context)
        {
            Context = context;
        }

        public async Task<MarcacaoServico> CreateProfissionalHorario(MarcacaoServico marcacaoServico)
        {
            var profHorario = await Context.AddAsync(marcacaoServico);
            await Context.SaveChangesAsync();
            return profHorario.Entity;
        }

    }
}
