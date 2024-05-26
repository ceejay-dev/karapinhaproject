using Karapinha.Model;
using Karapinha.Shared.IRepositories;
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

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            var user = await karapinhaDb.AddAsync(usuario);
            return user.Entity;
        }


    }
}
