using Amazon.Lambda.Model;
using Karapinha.DAL.Repositories;
using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinha.Shared.IServices;
using Microsoft.AspNetCore.Http;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CreateUser(Usuario usuario, IFormFile foto)
        {
            try
            {
                var usuarioAdicionado = await _usuarioRepository.CreateUser(usuario, foto);
                return usuarioAdicionado;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public async Task<Usuario> GetUserById(int id)
        {
            try
            {
                var user = await _usuarioRepository.GetUserById(id);
                if (user == null)
                {
                    throw new NotFoundException($"Usuário com ID {id} não encontrado.");
                }
                return user;
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


        public List<Usuario> GetAllUsers()
        {
            try
            {
                return _usuarioRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
    }
}
