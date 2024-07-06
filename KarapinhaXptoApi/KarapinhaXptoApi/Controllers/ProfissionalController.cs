using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Services;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class ProfissionalController : ControllerBase
    {
        private readonly IProfissionalService Service;

        public ProfissionalController(IProfissionalService service)
        {
            Service = service;
        }

        [HttpPost]
        [Route("/CreateProfissional")]
        public async Task<ActionResult> CreateProfissional([FromForm]ProfissionalDTO dto, IFormFile foto)
        {
            try
            {
                var profAdded = await Service.CreateProfissional(dto, foto);
                return Ok(profAdded);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while saving the entity changes.");
            }
        }

        [HttpGet]
        [Route("/GetAllProfissional")]
        public async Task<IEnumerable<ProfissionalDTO>> GetAllProfissionals()
        {
            try
            {
                return await Service.GetAllProfissionals();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetProfissionalById")]
        public async Task<ProfissionalDTO> GetProfissionalById(int id)
        {
            try
            {
                return await Service.GetProfissionalById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("/GetProfissionalByIdCategoria")]
        public async Task<ProfissionalDTO> GetProfissionalByIdCategoria(int id)
        {
            try
            {
                return await Service.GetProfissionalByIdCategoria(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteProfissional")]
        public async Task<ActionResult> DeleteProfissional(int id)
        {
            try
            {
                await Service.DeleteProfissional(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetAllProfissinalsByIdCategoria")]
        public Task<IEnumerable<dynamic>> GetAllProfissionaisByIdCategoria(int id)
        {
            try
            {
                return Service.GetAllProfissionaisByIdCategoria(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllProfissionaisWithCategories")]
        public IEnumerable<dynamic> GetAllProfissionaisWithCategoria()
        {
            try
            {
                return Service.GetAllProfissionaisWithCategoria();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}

