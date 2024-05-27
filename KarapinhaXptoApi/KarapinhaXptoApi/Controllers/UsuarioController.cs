using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Microsoft.AspNetCore.Mvc;

namespace KarapinhaXptoApi.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioService;

        public UsuarioController(IUsuarioRepository usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("/AddUser")]
        public async Task<ActionResult> CreateUser([FromForm] Usuario usuario, IFormFile foto)
        {
            try
            {
                var usuarioAdicionado = await _usuarioService.CreateUser(usuario, foto);
                return Ok(usuarioAdicionado);
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
        public async Task<Usuario> GetUserById(int id)
        {
            try
            {
                return await _usuarioService.GetUserById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return StatusCode(500, "Erro interno ao adicionar usuário.");
            }
        }
    }
}
