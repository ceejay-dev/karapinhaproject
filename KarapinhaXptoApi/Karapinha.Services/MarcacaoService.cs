using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.DAL.Repositories;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinha.Shared.IEmail;
using Karapinnha.DTO.Marcacao;
using OpenQA.Selenium;
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
        private readonly IEmailService emailService;
        private readonly IUtilizadorRepository utilizadorRepository;

        public MarcacaoService(IMarcacaoRepository repos, IEmailService emailService, IUtilizadorRepository utilizadorRepository)
        {
            repository = repos;
            this.emailService = emailService;
            this.utilizadorRepository = utilizadorRepository;
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
                var allUserBookings = await repository.GetAllBookingsByUserId(idUtilizador);
                return allUserBookings.Select(MarcacaoConverter.ToMarcacaoGetDTO);
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

        public async Task<bool> ConfirmBooking(int id)
        {
            try
            {
                var booking = await repository.GetBookingById(id);

                if (booking == null)
                {
                    return false;
                }
                else
                {
                    booking.Estado = "validado";
                    await repository.UpdateBooking(booking);

                    // Enviar e-mail de confirmação
                    var user = await utilizadorRepository.GetUserById(booking.FkUtilizador); 
                    if (user == null) throw new NotFoundException();

                    string subject = "Confirmação de Marcação";
                    string mensagem = $"A sua marcação foi confirmada. Por favor, compareça na data {booking.DataMarcacao} para ser atendido(a).";

                    emailService.SendEmailAdminOrCliente(mensagem, subject, UtilizadorConverter.ToUtilizadorDTO(user));

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message + ex.ToString());
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
