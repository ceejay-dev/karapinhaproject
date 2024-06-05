using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class HorarioController : ControllerBase
    {
        private readonly IHorarioService HorarioService;

        public HorarioController(IHorarioService horarioService)
        {
            HorarioService = horarioService;
        }

        [HttpPost]
        [Route("/AddSchedule")]
        public async Task<ActionResult> CreateSchedule(HorarioDTO dto)
        {
            var horario = await HorarioService.CreateSchedule(dto);
            return Ok(horario);
        }
    }
}
