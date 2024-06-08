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
    }
}
