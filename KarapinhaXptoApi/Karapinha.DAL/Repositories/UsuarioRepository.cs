using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly KarapinhaDbContext karapinhaDb;

        public UsuarioRepository(KarapinhaDbContext context)
        {
            karapinhaDb = context;
        }

        public async Task<Usuario> CreateUser(Usuario usuario, IFormFile foto)
        {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
                string fileName = Path.GetFileName(foto.FileName);
                string filePath = Path.Combine(imagePath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await foto.CopyToAsync(fileStream);
                }
                usuario.FotoUsuario = fileName;

                var user = await karapinhaDb.AddAsync(usuario);
                await karapinhaDb.SaveChangesAsync();
                return user.Entity;
        }

        public async Task<Usuario> GetUserById(int id)
        {
            try
            {
                var user = await karapinhaDb.FindAsync<Usuario>(id);
                return user;
            }
            catch (Exception ex)
            {
                // Qualquer exceção é encapsulada em uma DatabaseException e relançada.
                throw new Exception("Erro ao buscar usuário por ID no banco de dados.", ex);
            }
        }


        public List <Usuario> GetAllUsers()
        {
           var users = karapinhaDb.Usuarios.ToList();
            return users;
        }

    }
}
