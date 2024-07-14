using Karapinha.Model;
using Karapinnha.DTO.Marcacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IMarcacaoService
    {
        Task<MarcacaoDTO> CreateBooking(MarcacaoDTO marcacao);
        Task<MarcacaoGetDTO> GetBookingById(int id);
        Task<IEnumerable<MarcacaoGetDTO>> GetAllBookingByUserId(int idUtilizador);
        IEnumerable<MarcacaoGetDTO> GetAllBookings();
        Task<bool> ConfirmBooking(int id);
        Task<bool> RescheduleBooking(int id, DateOnly data);
        Task<IEnumerable<MarcacaoGetDTO>> GetBookingsByMonth();
        public double GetTotalAmountToday();
        public double GetTotalAmountYesterday();
        public double GetTotalAmountCurrentMonth();
        public double GetTotalAmountPastMonth();
        Task<bool> CancelBooking(int id);
    }
}
