using Karapinha.Shared.IServices;
using Karapinnha.DTO;
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
        public async Task<ActionResult> CreateBooking(MarcacaoDTO marcacao, MarcacaoServicoDTO marcacaoServico)
        {
            try
            {
                var booking = await bookingService.CreateBooking(marcacao, marcacaoServico); 
                return Ok(booking);
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            } 
        }
    }
}
