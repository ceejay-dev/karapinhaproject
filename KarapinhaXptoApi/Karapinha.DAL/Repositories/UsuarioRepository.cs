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
                var user = await karapinhaDb.Usuarios.FindAsync(id);
                if (user != null)
                    return user;

                return null;
            }
            catch (Exception ex)
            {
                // Qualquer exceção é encapsulada em uma DatabaseException e relançada.
                throw new Exception("User Not Found.", ex);
            }
        }


        public List<Usuario> GetAllUsers()
        {
            var users = karapinhaDb.Usuarios.ToList();
            return users;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                karapinhaDb.Usuarios.Remove(user);
                await karapinhaDb.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;                
            }
        }

        public async Task<Usuario> UpdateUser(Usuario usuario, int id)
        {
            /* var taskToUpdate = _context.Tasks.Find(id);

            taskToUpdate.Name = task.Name;
            taskToUpdate.Done = task.Done;
            _context.Entry(taskToUpdate).State = EntityState.Modified;
            _context.SaveChanges();*/
            var user = await karapinhaDb.Usuarios.FindAsync(id);
            if (user != null)
            {
                if(usuario.NomeUsuario != null)
                {
                    user.NomeUsuario = usuario.NomeUsuario;
                }if(usuario.EmailUsuario != null)
                {
                    user.EmailUsuario = usuario.EmailUsuario;
                }if(usuario.TelemovelUsuario != null)
                {
                    user.TelemovelUsuario = usuario.TelemovelUsuario;
                }if(usuario.UsernameUsuario != null)
                {
                    user.UsernameUsuario = usuario.UsernameUsuario;
                }if(usuario.PasswordUsuario != null)
                {
                    user.PasswordUsuario = usuario.PasswordUsuario;
                }
                karapinhaDb.Usuarios.Update(user);
                await karapinhaDb.SaveChangesAsync();
                return user;
            } else
            {
                throw new Exception("User Not Found");
            }
        }

    }
}
