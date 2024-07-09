﻿using Karapinha.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IRepositories
{
    public interface IMarcacaoRepository
    {
        Task<Marcacao> CreateBooking(Marcacao marcacao);
        Task<Marcacao> GetBookingById(int id);
        Task<IEnumerable<dynamic>> GetAllBookingsByUserId(int idUtilizador);
        IEnumerable<Marcacao> GetAllBookings();
        Task<bool> DeleteBooking(int id);
        Task UpdateBooking(Marcacao marcacao);
    }
}
