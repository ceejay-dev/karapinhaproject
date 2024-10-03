using Karapinha.DAL.Converters;
using Karapinha.Model;
using Karapinha.Services;
using Karapinha.Shared.IEmail;
using Karapinha.Shared.IServices;
using Karapinnha.DTO.Utilizador;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace KarapinhaXptoApi.Controllers
{
    public class UtilizadorController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilizadorService _UtilizadorService;

        public UtilizadorController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IUtilizadorService UtilizadorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _UtilizadorService = UtilizadorService;
        }

        [HttpPost]
        [Route("/CreateUser")]
        public async Task<ActionResult> CreateUser([FromForm] UtilizadorDTO Utilizador, IFormFile foto)
        {
            var user = new IdentityUser { UserName = Utilizador.UsernameUtilizador };
            var result = await _userManager.CreateAsync(user, Utilizador.PasswordUtilizador);

            if (result.Succeeded)
            {
                //Utilizador.PasswordUtilizador = ;
                await _UtilizadorService.CreateUser(Utilizador, foto);
                return Ok(new { Result = "Utilizador criado com sucesso!!" });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var user = await _UtilizadorService.Login(loginDto);
                return Ok(user);

            }
            catch
            {
                return StatusCode(401, "Credenciais incorrectas");
            }
           
        }

        [HttpGet]
        [Route("/GetUser")]
        public async Task<ActionResult<UtilizadorDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _UtilizadorService.GetUserById(id);
                return Ok(user); // Retorna Ok/200 usuário encontrado
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetUserByUsername")]
        public async Task<ActionResult<UtilizadorDTO>> GetUserByUsername(string username)
        {
            try
            {
                return await _UtilizadorService.GetUserByUsername(username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetIdByUsername")]
        public async Task<ActionResult> GetUserIdByUsername(string username)
        {
            try
            {
                var userId = await _UtilizadorService.GetUserIdByUsername(username);
                return Ok(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpGet]
        [Route("/GetUsers")]
        public Task<IEnumerable<UtilizadorDTO>> GetAllUsers()
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

        [HttpGet]
        [Route("/GetClients")]
        public Task<IEnumerable<UtilizadorDTO>> GetAllClientes()
        {
            try
            {
                return _UtilizadorService.GetAllClientes();
            }
            catch
            {
                throw new Exception();
            }
        }

        [HttpGet]
        [Route("/GetAdministratives")]
        public Task<IEnumerable<UtilizadorDTO>> GetAllAdministratives()
        {
            try
            {
                return _UtilizadorService.GetAllAdministratives();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro apagando o utilizador!");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro actualizando os dados do utilizador.");
            }
        }

        [HttpPut]
        [Route("/ActivateOrDesactivate")]
        public async Task<ActionResult> ActivateOrDesactivateClient(int id)
        {
            try
            {
                await _UtilizadorService.ActivateOrDesactivateClient(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetUserRole")]
        public async Task<ActionResult<string>> GetUserRole(string username)
        {
            try
            {
                var user = await _UtilizadorService.GetUserRole(username);
                return user;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("/ActivateAndChangePassword")]
        public async Task<ActionResult> ActivateAndChangePassword(string username, string password)
        {
            try
            {
                await _UtilizadorService.ActivateAndChangePassword(username, password);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/VerifyAdministrativeStatus")]
        public async Task<ActionResult> VerifyAdministrativeStatus(UtilizadorDTO dto)
        {
            try
            {
                await _UtilizadorService.VerifyAdministrativeStatus(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Por terminar
        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /*[HttpPost]
        [Route("/Login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(dto.Username);
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }*/
        //Utilizador.PasswordUtilizador = user.PasswordHash;
    }
}
