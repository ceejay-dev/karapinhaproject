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
        private readonly KarapinhaDbContext dbContext;

        public UtilizadorRepository(KarapinhaDbContext context)
        {
            dbContext = context;
        }

        public async Task<Utilizador> CreateUser(Utilizador Utilizador)
        {
            var user = await dbContext.AddAsync(Utilizador);
            await dbContext.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<Utilizador> GetUserById(int id)
        {
            return await dbContext.Utilizadores.FindAsync(id);
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            var user = await dbContext.Utilizadores
                      .Where(u => u.UsernameUtilizador == username)
                      .Select(u => u.IdUtilizador)
                      .FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<Utilizador>> GetAllUsers()
        {
            return await dbContext.Utilizadores.ToListAsync();
        }

        public async Task<List<Utilizador>> GetAllClientes()
        {
            return await dbContext.Utilizadores
                .Where(u => u.TipoPerfil == "cliente")
                .ToListAsync();
        }

        public async Task<List<Utilizador>> GetAllAdministratives()
        {
            return await dbContext.Utilizadores
                .Where(u => u.TipoPerfil == "administrativo")
                .ToListAsync();
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

        public async Task<Utilizador> GetUserByUsername(string username)
        {
            return await dbContext.Utilizadores.SingleOrDefaultAsync(u => u.UsernameUtilizador == username);
        }

        public bool VerifyStatus (Utilizador utilizador)
        {
            return (utilizador.Estado == "activo");
        }

        public async Task<bool> VerifyAdministrativeStatus(Utilizador user)
        {
            return (user.TipoPerfil == "administrativo" && user.Estado == "activo");
        }

        public async Task <string> GetUserRole (string username)
        {
            var tipoPerfil = dbContext.Utilizadores
            .Where(u => u.UsernameUtilizador == username)
            .Select(u => u.TipoPerfil)
            .FirstOrDefault();

            return tipoPerfil;
        }

       
    }
}
