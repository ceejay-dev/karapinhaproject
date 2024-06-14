using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenQA.Selenium;

namespace KarapinhaXptoApi.Controllers
{
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService Servico;

        public ServicoController(IServicoService servico)
        {
            Servico = servico;
        }

        [HttpPost]
        [Route("/CreateTreatment")]
        public async Task<ActionResult> CreateTreatment(ServicoDTO dto)
        {
            try
            {
                var tretment = await Servico.CreateTreatment(dto);
                return Ok(tretment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet]
        [Route("/GetAllTreatments")]
        public async Task<IEnumerable<ServicoDTO>> GetAllTreatments()
        {
            try
            {
                return await Servico.GetAllTreatments();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetTreatmentById")]
        public async Task<ServicoDTO> GetTreatmentsById(int id)
        {
            try
            {
                 return await Servico.GetTreatementById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteTreatment")]
        public async Task<ActionResult> DeleteTreatment(int id)
        {
            try
            {
                await Servico.DeleteTreatment(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("/UpdateTreatment")]
        public async Task<ActionResult> UpdatTreatment(ServicoUpdateDTO dto)
        {
            try {
                await Servico.UpdateTreatment(dto);
                return NoContent();
            } catch (Exception ex) { 
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet]
        [Route("/GetAllServicosByIdCategoria")]
        public IEnumerable<dynamic> GetAllServicosByIdCategoria()
        {
            try
            {
                return Servico.GetAllServicosByIdCategoria();
            }
            catch (Exception ex) {
                throw new Exception (ex.Message);
            }
        }
    }
}
