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
            return await DbContext.Marcacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Marcacao>> GetAllBookings()
        {
            return await DbContext.Marcacoes.ToListAsync();
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await GetBookingById(id);

            if (booking != null)
            {
                DbContext.Marcacoes.Remove(booking);
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task UpdateBooking(Marcacao marcacao)
        {
            DbContext.Marcacoes.Update(marcacao);
            await DbContext.SaveChangesAsync();
        }

    }
}
