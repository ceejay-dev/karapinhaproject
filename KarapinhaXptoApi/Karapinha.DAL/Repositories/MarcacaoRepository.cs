using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinnha.DTO.Marcacao;
using Karapinnha.DTO.Servico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class MarcacaoRepository : IMarcacaoRepository
    {
        private readonly KarapinhaDbContext DbContext;

        public MarcacaoRepository(KarapinhaDbContext context)
        {
            DbContext = context;
        }

        public async Task<Marcacao> CreateBooking(Marcacao marcacao)
        {
            var booking = await DbContext.AddAsync(marcacao);
            await DbContext.SaveChangesAsync();
            return booking.Entity;
        }


        public async Task<Marcacao> GetBookingById(int id)
        {
            return await DbContext.Marcacoes
                .Include(m => m.Utilizador)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Service)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Horario)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Profissional)
                .FirstOrDefaultAsync(m => m.IdMarcacao == id);
        }

        public async Task<IEnumerable<Marcacao>> GetAllBookingsByUserId(int idUtilizador)
        {
            return await DbContext.Marcacoes
                .Include(m => m.Utilizador)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Service)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Horario)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Profissional)
                .Where(m => m.FkUtilizador == idUtilizador)
                .ToListAsync();
        }

        public IEnumerable<Marcacao> GetAllBookings()
        {
            return DbContext.Marcacoes
                .Include(m => m.Utilizador)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Service)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Horario)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Profissional)
                .ToList();
        }

        public async Task UpdateBooking(Marcacao marcacao)
        {
            DbContext.Marcacoes.Update(marcacao);
            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Marcacao>> GetBookingsByMonth()
        {
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var startDateOfMonth = new DateOnly(currentDate.Year, currentDate.Month, 1);
            var endDateOfMonth = startDateOfMonth.AddMonths(1).AddDays(-1);

            var monthlyBookings = DbContext.Marcacoes
                .Where(m => m.DataMarcacao >= startDateOfMonth && m.DataMarcacao <= endDateOfMonth)
                .Include(m => m.Utilizador)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Service)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Horario)
                .Include(m => m.Servicos)
                    .ThenInclude(s => s.Profissional)
                .OrderBy(m => m.DataMarcacao)
                .ToList();

            return monthlyBookings;
        }


    }
}
