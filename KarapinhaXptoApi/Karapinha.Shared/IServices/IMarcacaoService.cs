using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Shared.IServices
{
    public interface IMarcacaoService
    {
        Task<MarcacaoDTO> CreateBooking(MarcacaoDTO marcacao, MarcacaoServicoDTO marcacaoServico);
        /*Task<MarcacaoDTO> GetBookingById(int id);
        Task<IEnumerable<MarcacaoDTO>> GetAllBookings();
        Task<bool> DeleteBooking(int id);
        Task UpdateBooking(MarcacaoDTO marcacao);*/
    }
}
