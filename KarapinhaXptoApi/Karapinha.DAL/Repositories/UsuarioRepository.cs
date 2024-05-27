using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try {
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
            } catch (Exception ex) {
                Console.WriteLine(ex.Message, "Erro ao inserir o usuário na base de dados");
            }
        }

        public async Task<Usuario> GetUserById(int id)
        {
            try
            {
                return await karapinhaDb.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Erro ao pesquisar o usuário na base de dados");
                // Retorne null ou uma exceção adequada
                return null;
            }
        }



    }
}
