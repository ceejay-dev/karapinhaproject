using Amazon.Lambda.Model;
using Karapinha.DAL.Converters;
using Karapinha.DAL.Repositories;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class UtilizadorService : IUtilizadorService
    {
        private readonly IUtilizadorRepository UtilizadorRepository;

        public UtilizadorService(IUtilizadorRepository usuarioRepository)
        {
            UtilizadorRepository = usuarioRepository;
        }

        public async Task<UtilizadorDTO> CreateUser(UtilizadorDTO Utilizador, IFormFile foto)
        {
            try
            {
                var utilizador = UtilizadorConverter.ToUtilizador(Utilizador);
                utilizador.EncriptarPassword(Utilizador.PasswordUtilizador);

                var usuarioAdded = UtilizadorConverter.ToUtilizadorDTO(await UtilizadorRepository.CreateUser(UtilizadorConverter.ToUtilizador(Utilizador), foto));
                return usuarioAdded;
            }
            catch (Exception ex) { 
                throw new ServiceException(ex.Message);
            }
        }

        public async Task<UtilizadorDTO> Login(string username, string password)
        {
            try
            {
                var user = await UtilizadorRepository.GetUserByUsername(username);
                if (user == null || !user.VerificarPassword(password) || !UtilizadorRepository.VerifyState(user))
                {
                    throw new NotFoundException();
                }
                return UtilizadorConverter.ToUtilizadorDTO(user);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.ToString());
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
                throw new ServiceException("Erro ao obter usuário por ID.", ex);
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
            catch (Exception ex)
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
//var activationLink = $"https://localhost:7209/ActivateOrDesactivate?id={usuarioAdded.IdUtilizador}";
//await emailService.SendActivationEmail("candidojoao12@gmail.com", "Ativação de Conta", $"Ative a sua conta clicando no link: {activationLink}");
