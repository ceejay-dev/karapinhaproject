using Karapinha.Model;
using Karapinha.Shared.IRepositories;
using Karapinnha.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL.Repositories
{
    public class UtilizadorRepository : IUtilizadorRepository
    {
        public readonly KarapinhaDbContext dbContext;

        public UtilizadorRepository(KarapinhaDbContext context)
        {
            dbContext = context;
        }

        public async Task<Utilizador> CreateUser(Utilizador Utilizador, IFormFile foto)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Storage");
            string fileName = Path.GetFileName(foto.FileName);
            string filePath = Path.Combine(imagePath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(fileStream);
            }
            Utilizador.FotoUtilizador = fileName;

            var user = await dbContext.AddAsync(Utilizador);
            await dbContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<Utilizador> GetUserById(int id)
        {
            return await dbContext.Utilizadores.FindAsync(id);
        }


        public async Task<List<Utilizador>> GetAllUsers()
        {
            return await dbContext.Utilizadores.ToListAsync();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                dbContext.Utilizadores.Remove(user);
                await dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateUser(Utilizador Utilizador)
        {
            dbContext.Utilizadores.Update(Utilizador);
            await dbContext.SaveChangesAsync();
        }

    }
}
