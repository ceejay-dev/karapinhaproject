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
    public class ServicoRepository : IServicoRepository
    {
        private readonly KarapinhaDbContext DbContext;

        public ServicoRepository(KarapinhaDbContext context)
        {
            DbContext = context;
        }

        public async Task<Servico> CreateTreatment(Servico servico)
        {
            var treatmentAdded = await DbContext.AddAsync(servico);
            await DbContext.SaveChangesAsync();
            return treatmentAdded.Entity;
        }

        public async Task<Servico> GetTreatmentById(int id)
        {
            return await DbContext.Servicos.FindAsync(id);
        }

        public async Task<List<Servico>> GetAllTreatments()
        {
            return await DbContext.Servicos.ToListAsync();
        }

        public async Task<bool> DeleteTreatment(int id)
        {
            var treatment = await GetTreatmentById(id);

            if (treatment != null)
            {
                DbContext.Servicos.Remove(treatment);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateTreatment(Servico servico)
        {
            DbContext.Servicos.Update(servico);
            await DbContext.SaveChangesAsync();
        }

        public IEnumerable<dynamic> GetAllServicosByIdCategoria()
        {
            var result = from servico in DbContext.Servicos
                         join categoria in DbContext.Categorias
                         on servico.FkCategoria equals categoria.IdCategoria
                         select new
                         {
                             IdServico = servico.IdServico,
                             NomeServico = servico.NomeServico,
                             Preco = servico.Preco,
                             FkCategoria = servico.FkCategoria,
                             NomeCategoria = categoria.NomeCategoria
                         };

            return result.ToList();
        }

        public async Task<IEnumerable<Servico>> GetMostRequestedTreatments()
        {
            var mostRequestedServices = await DbContext.MarcacaoServicos
                .GroupBy(ms => ms.FkServico)
                .Select(g => new
                {
                    FkServico = g.Key,
                    TotalSolicitacoes = g.Count()
                })
                .ToListAsync();

            var servicesWithCounts = mostRequestedServices
                .Join(
                    DbContext.Servicos,
                    ms => ms.FkServico,
                    s => s.IdServico,
                    (ms, s) => new Servico
                    {
                        NomeServico = s.NomeServico,
                        Contador = ms.TotalSolicitacoes
                    }
                )
                .OrderByDescending(x => x.Contador)
                .ToList();

            return servicesWithCounts;
        }


    }
}
