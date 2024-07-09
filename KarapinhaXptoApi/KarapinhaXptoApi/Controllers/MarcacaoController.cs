﻿using Karapinha.Shared.IServices;
using Karapinnha.DTO.Marcacao;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class MarcacaoController : ControllerBase
    {
        private readonly IMarcacaoService bookingService;

        public MarcacaoController(IMarcacaoService marcacaoService)
        {
            this.bookingService = marcacaoService;
        }

        [HttpPost]
        [Route("/CreateBooking")]
        public async Task<ActionResult<MarcacaoDTO>> CreateBooking([FromBody] MarcacaoDTO marcacao)
        {
            try
            {
                var booking = await bookingService.CreateBooking(marcacao);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.ToString());
                //return StatusCode(500, "{\"mensagem\":\"Erro interno ao adicionar a marcação.\"}");
            }
        }

        [HttpGet]
        [Route("/GetBookingById")]
        public async Task<ActionResult<MarcacaoDTO>> GetBookingById(int id)
        {
            try
            {
                var booking = await bookingService.GetBookingById(id);
                return Ok(booking);
            }
            catch
            {
                return StatusCode(404, "Marcação não foi encontrada.");
            }
        }

        [HttpGet]
        [Route("/GetAllBookingByUserId")]
        public async Task<IEnumerable<dynamic
            
            
            >> GetAllBookingByUserId(int id)
        {
            try
            {
                return await bookingService.GetAllBookingByUserId(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Marcações não foram encontradas. {ex.Message}");
            }
        }

        [HttpGet]
        [Route("/GetAllBookings")]
        public IEnumerable<MarcacaoGetDTO> GetAllBookings()
        {
            try
            {
                return bookingService.GetAllBookings();
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"Marcações não foram encontradas. {ex.Message}");
                }
            }
        }

        [HttpPut]
        [Route("/ConfirmBooking")]
        public async Task<ActionResult> ConfirmBooking (int id)
        {
            try
            {
                var result =  await bookingService.ConfirmBooking(id);
                return Ok(result);
            }
            catch (Exception ex) { 
                throw new Exception (ex.Message);
            }
        }
    }
}
