using Amazon.Lambda.Model;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("/AddUser")]
        public async Task<ActionResult> CreateUser([FromForm] Usuario usuario, IFormFile foto)
        {
            try
            {
                var userAdded = await _usuarioService.CreateUser(usuario, foto);
                return Ok(userAdded);
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "Erro interno ao adicionar usuário.");
                }
            }
        }

        [HttpGet]
        [Route("/GetUser")]
        public async Task<ActionResult<Usuario>> GetUserById(int id)
        {
            try
            {
                var user = await _usuarioService.GetUserById(id);
                return Ok(user); // Retorna 200 OK com o usuário encontrado
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); // Retorna 404 Not Found com a mensagem de erro específica
            }
        }


        [HttpGet]
        [Route("/GetUsers")]
        public List<Usuario> GetUsers()
        {
            try
            {
                return _usuarioService.GetAllUsers();

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
                await _usuarioService.DeleteUser(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting an user!");
            }
        }

        [HttpPut]
        [Route("/UpdateUser")]
        public async Task<ActionResult> UpdateUser(Usuario usuario, int id)
        {
            try
            {
                var userUpdated = await _usuarioService.UpdateUser(usuario, id);
                return Ok(userUpdated);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting an user!,");
            }
        }
    }
}
