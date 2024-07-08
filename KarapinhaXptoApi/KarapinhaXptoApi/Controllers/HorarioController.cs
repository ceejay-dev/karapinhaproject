using Karapinha.Model;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Horario;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.DevTools.V123.Page;
using OpenQA.Selenium.DevTools.V123.Runtime;

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
            try
            {
                var horario = await HorarioService.CreateSchedule(dto);
                return Ok(horario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("/GetAllSchedules")]
        public async Task<IEnumerable<HorarioDTO>> GetAllSchedules()
        {
            try
            {
                return await HorarioService.GetAllSchedules();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet]
        [Route("/GetSchedule")]
        public async Task<ActionResult<HorarioDTO>> GetScheduleById(int id)
        {
            try
            {
                return await HorarioService.GetScheduleById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetAllSchedulesByProfissionalId")]
        public async Task<IEnumerable<HorarioDTO>> GetAllSchedulesByProfissionalId(int profissionalId)
        {
            try
            {
                return await HorarioService.GetAllSchedulesByProfissionalId(profissionalId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
