using Karapinha.Shared.IServices;
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
        public async Task<ActionResult> CreateBooking([FromBody]MarcacaoDTO marcacao)
        {
            try
            {
                var booking = await bookingService.CreateBooking(marcacao); 
                return Ok(booking);
            }
            catch  {
                return StatusCode(500, "Erro ao criar a marcação.");
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
        [Route("/GetAllBookings")]
        public IEnumerable<MarcacaoGetDTO> GetAllBookings()
        {
            try
            {
                return bookingService.GetAllBookings();
            }
            catch
            {
                throw new Exception("Marcações não foram encontradas.");
            }
        }
    }
}
