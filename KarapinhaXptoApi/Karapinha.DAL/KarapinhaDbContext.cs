using Karapinha.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Karapinha.DAL
{
    public class KarapinhaDbContext : IdentityDbContext<IdentityUser>
    {
        public KarapinhaDbContext(DbContextOptions<KarapinhaDbContext> options)
        : base(options)
        {
        }
        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marcacao> Marcacoes { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<ProfissionalHorario> ProfissionalHorarios { get; set; }
        public DbSet<MarcacaoServico> MarcacaoServicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //uma categoria pode ter muitos servicos
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Servicos)
                .WithOne(c => c.Categoria)
                .HasForeignKey(p => p.FkCategoria)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
