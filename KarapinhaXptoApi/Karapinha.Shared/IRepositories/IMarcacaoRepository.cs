﻿using Karapinha.Model;
using Karapinnha.DTO.Marcacao;
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
        Task<IEnumerable<Marcacao>> GetAllBookingsByUserId(int idUtilizador);
        IEnumerable<Marcacao> GetAllBookings();
        Task<IEnumerable<Marcacao>> GetBookingsByMonth();
        Task UpdateBooking(Marcacao marcacao);
        public double GetTotalAmountToday();
        public double GetTotalAmountYesterday();
        public double GetTotalAmountCurrentMonth();
        public double GetTotalAmountPastMonth();

    }
}
