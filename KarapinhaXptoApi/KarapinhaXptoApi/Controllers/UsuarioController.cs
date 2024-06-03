using Karapinha.DAL.Converters;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class UtilizadorController : ControllerBase
    {
        private readonly IUtilizadorService _UtilizadorService;

        public UtilizadorController(IUtilizadorService UtilizadorService)
        {   
            _UtilizadorService = UtilizadorService;
        }

        [HttpPost]
        [Route("/AddUser")]
        public async Task<ActionResult> CreateUser([FromForm] UtilizadorCreateDTO Utilizador, IFormFile foto)
        {
            try
            {
                var userAdded = await _UtilizadorService.CreateUser(Utilizador, foto);
                return Ok(userAdded);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Error adding the user.");
                }
            }
        }

        [HttpGet]
        [Route("/GetUser")]
        public async Task<ActionResult<UtilizadorCreateDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _UtilizadorService.GetUserById(id);
                return Ok(user); // Retorna Ok/200 usuário encontrado
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Retorna 404 Not Found com a mensagem de erro específica
            }
        }


        [HttpGet]
        [Route("/GetUsers")]
        public Task<IEnumerable<UtilizadorCreateDTO>> GetAllUsers()
        {
            try
            {
                return _UtilizadorService.GetAllUsers();
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpDelete]
        [Route("/DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _UtilizadorService.DeleteUser(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting an user!");
            }
        }

        [HttpPut]
        [Route("/UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] UtilizadorUpdateDTO Utilizador)
        {
            try
            {
                await _UtilizadorService.UpdateUser(Utilizador);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating an user!,");
            }
        }
    }
}
