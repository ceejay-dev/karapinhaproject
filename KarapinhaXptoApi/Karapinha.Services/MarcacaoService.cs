using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class MarcacaoService : IMarcacaoService
    {
        private readonly IMarcacaoRepository repository;
        private readonly IMarcacaoServicosRepository servicoRepository;

        public MarcacaoService(IMarcacaoRepository repos)
        {
            repository = repos;
        }

        public async Task<MarcacaoDTO> CreateBooking(MarcacaoDTO booking, MarcacaoServicoDTO bookingService)
        {
            var marcacaoAdded = MarcacaoConverter.ToMarcacaoDTO(await repository.CreateBooking(MarcacaoConverter.ToMarcacao(booking)));

            var marcacaoServico = new MarcacaoServico()
            {
                FkMarcacao = booking.IdMarcacao,
                FkCategoria = bookingService.FkCategoria,
                FkServico = bookingService.FkServico,
                FkProfissional = bookingService.FkProfissional,
            };
            await servicoRepository.CreateMarcacaoServico(marcacaoServico);

            return marcacaoAdded;

        }
        /*public async Task<MarcacaoDTO> GetBookingById(int id)
        {

        }   
        public async Task<IEnumerable<MarcacaoDTO>> GetAllBookings()
        {

        }
        public async Task<bool> DeleteBooking(int id)
        {

        }
        public async Task UpdateBooking(MarcacaoDTO marcacao)
        {

        }*/
    }
}
