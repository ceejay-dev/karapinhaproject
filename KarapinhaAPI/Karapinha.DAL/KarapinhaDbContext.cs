using Karapinha.Model;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL
{
    public class KarapinhaDbContext : DbContext
    {
        public KarapinhaDbContext(DbContextOptions<KarapinhaDbContext> options)
        : base(options)
        {
        }
        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet <Servico> Servicos { get; set; }
        public DbSet <Profissional> Profissionais { get; set; }
        public DbSet <Categoria> Categorias { get; set; }
        public DbSet <Marcacao> Marcacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //uma marcação pode ter muitos serviços
            modelBuilder.Entity<Marcacao>()
                .HasMany(s=> s.Servicos)
                .WithOne(s => s.Marcacao)
                .HasForeignKey(s => s.IdMarcacao)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
