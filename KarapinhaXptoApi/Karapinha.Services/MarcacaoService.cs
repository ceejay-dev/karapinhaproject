using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Marcacao;
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

        public MarcacaoService(IMarcacaoRepository repos)
        {
            repository = repos;
        }

        public async Task<MarcacaoDTO> CreateBooking(MarcacaoDTO booking)
        {
            try
            {
                var marcacaoAdded = MarcacaoConverter.ToMarcacaoDTO(await repository.CreateBooking(MarcacaoConverter.ToMarcacao(booking)));
                return marcacaoAdded;
            }
            catch (ServiceException ex) { 
                throw new ServiceException(ex);
            }

        }
        public async Task<MarcacaoGetDTO> GetBookingById(int id)
        {
            try
            {
                return MarcacaoConverter.ToMarcacaoGetDTO(await repository.GetBookingById(id));
            }
            catch (ServiceException ex) {
                throw new ServiceException($"Marcação não foi encontrada {ex.Message}");
            }
        }

        public async Task<IEnumerable<MarcacaoGetDTO>> GetAllBookingByUserId(int idUtilizador)
        {
            try
            {
                var allBookings = await repository.GetAllBookingsByUserId(idUtilizador);
                return allBookings.Select(MarcacaoConverter.ToMarcacaoGetDTO);
            }
            catch (ServiceException ex) {
                throw new ServiceException (ex.Message);
            }
        }
        public IEnumerable<MarcacaoGetDTO> GetAllBookings()
        {
            try
            {
                var allBookings =  repository.GetAllBookings();
                return allBookings.Select(MarcacaoConverter.ToMarcacaoGetDTO);
            }
            catch (ServiceException ex) {
                throw new ServiceException($"Marcações não foram encontradas {ex.Message}");
            }
        }
        /*public async Task<bool> DeleteBooking(int id)
        {

        }
        public async Task UpdateBooking(MarcacaoDTO marcacao)
        {

        }*/
    }
}
