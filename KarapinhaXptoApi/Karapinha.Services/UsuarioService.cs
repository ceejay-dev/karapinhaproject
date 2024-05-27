using Amazon.Lambda.Model;
using Karapinha.DAL.Repositories;
using Karapinha.Model;
using Karapinha.Shared.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
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
                return user;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
    }
}