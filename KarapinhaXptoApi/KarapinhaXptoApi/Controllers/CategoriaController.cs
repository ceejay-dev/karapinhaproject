using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService service)
        {
            _categoriaService = service;
        }

        [HttpPost]
        [Route("/AddCategory")]
        public async Task<ActionResult> CreateCategory([FromForm] CategoriaDTO dto)
        {
            try
            {
                var categoryAdded = await _categoriaService.CreateCategory(dto);
                return Ok(categoryAdded);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro interno ao adicionar a categoria.");
                }
            }
        }

        [HttpGet]
        [Route("/GetCategory")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _categoriaService.GetCategoryById(id);
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
                return _categoriaService.GetAllCategories();
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
                await _categoriaService.DeleteCategory(id);
                return Ok();
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting a category!");
            }
        }

        [HttpPut]
        [Route("/UpdateCategory")]
        public async Task<ActionResult> UpdateCategory(CategoriaDTO dto)
        {
            try
            {
                await _categoriaService.UpdateCategory(dto);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating a category!");
            }
        }

    }
}
