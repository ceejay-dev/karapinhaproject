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
        IEnumerable<MarcacaoGetDTO> GetAllBookings();
        /*Task<bool> DeleteBooking(int id);
        Task UpdateBooking(MarcacaoDTO marcacao);*/
    }
}
