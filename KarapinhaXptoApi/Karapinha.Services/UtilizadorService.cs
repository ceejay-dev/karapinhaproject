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
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class UtilizadorService : IUtilizadorService
    {
        private readonly IUtilizadorRepository _UtilizadorRepository;

        public UtilizadorService(IUtilizadorRepository usuarioRepository)
        {
            _UtilizadorRepository = usuarioRepository;
        }

        public async Task<UtilizadorCreateDTO> CreateUser(UtilizadorCreateDTO Utilizador, IFormFile foto)
        {
            try
            {
                var usuarioAdded = UtilizadorConverter.ToUtilizadorDTO(await _UtilizadorRepository.CreateUser(UtilizadorConverter.ToUtilizador(Utilizador), foto));
                return usuarioAdded;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<UtilizadorCreateDTO> GetUserById(int id)
        {
            try
            {
                var user = await _UtilizadorRepository.GetUserById(id);
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


        public async Task<IEnumerable<UtilizadorCreateDTO>> GetAllUsers()
        {
            try
            {
                var utilizadores = await _UtilizadorRepository.GetAllUsers();
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
                await _UtilizadorRepository.DeleteUser(id);
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
                var user = await _UtilizadorRepository.GetUserById(utilizador.IdUtilizador);
                if (user == null) return;

                await _UtilizadorRepository.UpdateUser(UtilizadorConverter.UpdateUtilizador(utilizador, user));
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
    }
}
