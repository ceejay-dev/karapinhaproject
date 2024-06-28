using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.DAL.Repositories;
using Karapinha.Model;
using Karapinha.Shared.IEmail;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using MimeKit;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Karapinha.Services
{
    public class UtilizadorService : IUtilizadorService
    {
        private readonly IUtilizadorRepository UtilizadorRepository;
        private readonly IEmailService emailService;
        private readonly IEmailReceiver emailReceiver;
        public UtilizadorService(IUtilizadorRepository usuarioRepository, IEmailService emailService, IEmailReceiver emailReceiver)
        {
            UtilizadorRepository = usuarioRepository;
            this.emailService = emailService;
            this.emailReceiver = emailReceiver;
        }

        public async Task<UtilizadorDTO> CreateUser(UtilizadorDTO Utilizador, IFormFile foto)
        {
            try
            {
                //Armazenamento da fotografia do utilizador
                string photoPath = null;

                if (foto != null)
                {
                    var uploadsFolder = Path.Combine("wwwroot", "storage");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    photoPath = Path.Combine(uploadsFolder, Guid.NewGuid() + Path.GetExtension(foto.FileName));
                    using (var fileStream = new FileStream(photoPath, FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                    }
                    photoPath = "/" + photoPath.Replace("wwwroot\\", string.Empty).Replace("\\", "/");
                    
                }

                var userAdded = Utilizador;
                userAdded.FotoUtilizador = photoPath;
                userAdded  = UtilizadorConverter.ToUtilizadorDTO(await UtilizadorRepository.CreateUser(UtilizadorConverter.ToUtilizador(userAdded)));
                var userRole = await UtilizadorRepository.GetUserRole(Utilizador.UsernameUtilizador);

                //Envios de emails
                if (userRole == "cliente")
                {
                    //Enviando email para o admin ativar conta
                    emailReceiver.SendEmailAdmin(Utilizador.EmailUtilizador);
                }
                else if (userRole == "administrativo")
                {
                    string assunto = "Criação de uma nova conta de administrativo";
                    //Enviando email para o administrativos com as suas credenciais
                    string mensagem = "Bem-vindo ao Karapinha Dura XPTO" +
                       "Acabou de ser registrado(a) como administrador(a) do mesmo. " +
                       "Eis os dados as credenciais de acesso:" +
                       "Nome de utilizador :" + userAdded.UsernameUtilizador + "\n" +
                       "Palavra-passe    :" + userAdded.PasswordUtilizador;
                    emailService.SendEmailAdminOrCliente(mensagem, assunto, userAdded);
                }
                return userAdded;
            }
            catch (ServiceException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<UtilizadorDTO> Login(LoginDTO login)
        {
            try
            {
                var user = await UtilizadorRepository.GetUserByUsername(login.usernameUtilizador);
                if (user == null || !user.VerificarPassword(login.passwordUtilizador))
                {
                    throw new NotFoundException();
                }
                else if (user.TipoPerfil == "cliente" && !UtilizadorRepository.VerifyStatus(user))
                {
                    throw new Exception();
                }
                return UtilizadorConverter.ToUtilizadorDTO(user);
            }
            catch (ServiceException ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<UtilizadorDTO> GetUserById(int id)
        {
            try
            {
                var user = await UtilizadorRepository.GetUserById(id);
                if (user == null)
                {
                    throw new NotFoundException();
                }
                return UtilizadorConverter.ToUtilizadorDTO(user);
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Erro ao obter utilizador por ID.", ex);
            }
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            try
            {
                var idUser = await UtilizadorRepository.GetUserIdByUsername(username);
                if (idUser == null) { throw new NotFoundException(); }

                return idUser;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Utilizador não foi encontrado.", ex);
            }
        }

        public async Task<UtilizadorDTO> GetUserByUsername(string username)
        {
            try
            {

                var user = await UtilizadorRepository.GetUserByUsername(username);
                return UtilizadorConverter.ToUtilizadorDTO(user);

            }
            catch (Exception ex)
            {
                throw new ServiceException("Utilizador não foi encontrado.", ex);
            }
        }

        public async Task<IEnumerable<UtilizadorDTO>> GetAllUsers()
        {
            try
            {
                var utilizadores = await UtilizadorRepository.GetAllUsers();
                return utilizadores.Select(UtilizadorConverter.ToUtilizadorDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UtilizadorDTO>> GetAllClientes()
        {
            try
            {
                var utilizadores = await UtilizadorRepository.GetAllClientes();
                return utilizadores.Select(UtilizadorConverter.ToUtilizadorDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UtilizadorDTO>> GetAllAdministratives()
        {
            try
            {
                var utilizadores = await UtilizadorRepository.GetAllAdministratives();
                return utilizadores.Select(UtilizadorConverter.ToUtilizadorDTO);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                await UtilizadorRepository.DeleteUser(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task UpdateUser(UtilizadorUpdateDTO utilizador)
        {
            try
            {
                var user = await UtilizadorRepository.GetUserById(utilizador.IdUtilizador);
                if (user == null) return;

                await UtilizadorRepository.UpdateUser(UtilizadorConverter.UpdateUtilizador(utilizador, user));
            }
            catch (ServiceException ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task ActivateOrDesactivateClient(int id)
        {
            try
            {
                var userFound = await UtilizadorRepository.GetUserById(id);

                if (userFound == null) throw new NotFoundException();

                userFound.Estado = userFound.Estado == "inactivo" ? "activo" : "inactivo";
                string mensagem = "";
                string subject = "";
                if (userFound.Estado == "activo")
                {
                    subject = "Activação de conta";
                    mensagem = "A sua conta foi activada com sucesso";
                }
                else if (userFound.Estado == "inactivo")
                {
                    subject = "Desactivação de conta";
                    mensagem = "A sua conta foi desactivada. Aguarde a activação do administrador.";
                }

                emailService.SendEmailAdminOrCliente(mensagem, subject, UtilizadorConverter.ToUtilizadorDTO(userFound));
                await UtilizadorRepository.UpdateUser(userFound);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<bool> VerifyAdministrativeStatus(UtilizadorDTO dto)
        {
            try
            {
                return (await UtilizadorRepository.VerifyAdministrativeStatus(UtilizadorConverter.ToUtilizador(dto)));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }

        public async Task ActivateAndChangePassword(string username, string password)
        {
            try
            {
                var userFound = await UtilizadorRepository.GetUserByUsername(username);
                if (userFound == null) throw new NotFoundException();

                userFound.Estado = "activo";
                userFound.PasswordUtilizador = password;

                await UtilizadorRepository.UpdateUser(userFound);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }

        }

        public async Task<string> GetUserRole(string username)
        {
            try
            {
                var userRole = await UtilizadorRepository.GetUserRole(username);
                return userRole;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message);
            }
        }
    }
}