using Karapinha.Shared.IServices;
using Karapinnha.DTO.Categoria;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService CategoriaService;

        public CategoriaController(ICategoriaService service)
        {
            CategoriaService = service;
        }

        [HttpPost]
        [Route("/CreateCategory")]
        public async Task<ActionResult> CreateCategory(CategoriaDTO dto)
        {
            try
            {
                var categoryAdded = await CategoriaService.CreateCategory(dto);
                return Ok(categoryAdded);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "{\"mensagem\":\"Erro interno ao adicionar a categoria.\"}");
            }
        }

        [HttpGet]
        [Route("/GetCategory")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await CategoriaService.GetCategoryById(id);
                return Ok(category);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetAllCategories")]
        public Task<IEnumerable<CategoriaDTO>> GetAllCategories()
        {
            try
            {
                return CategoriaService.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {
                await CategoriaService.DeleteCategory(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro apagandoa a categoria!");
            }
        }

        [HttpPut]
        [Route("/UpdateCategory")]
        public async Task<ActionResult> UpdateCategory(CategoriaUpdateDTO dto)
        {
            try
            {
                await CategoriaService.UpdateCategory(dto);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro actualizando a categoria!");
            }
        }

    }
}
