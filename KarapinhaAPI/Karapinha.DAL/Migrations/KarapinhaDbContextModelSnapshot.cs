﻿// <auto-generated />
using System;
using Karapinha.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    [DbContext(typeof(KarapinhaDbContext))]
    partial class KarapinhaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Karapinha.Model.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("NomeCategoria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCategoria");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.Property<int>("IdMarcacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMarcacao"));

                    b.Property<DateTime>("DataMarcacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkUsuario")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraMarcacao")
                        .HasColumnType("datetime2");

                    b.HasKey("IdMarcacao");

                    b.HasIndex("FkUsuario");

                    b.ToTable("marcacoes");
                });

            modelBuilder.Entity("Karapinha.Model.Profissional", b =>
                {
                    b.Property<int>("IdProfissional")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfissional"));

                    b.Property<string>("BilheteProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkServico")
                        .HasColumnType("int");

                    b.Property<string>("FotoProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelemovelProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("horarios")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProfissional");

                    b.ToTable("profissionais");
                });

            modelBuilder.Entity("Karapinha.Model.Servico", b =>
                {
                    b.Property<int>("IdServico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServico"));

                    b.Property<int>("FkCategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdMarcacao")
                        .HasColumnType("int");

                    b.Property<string>("NomeServico")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("IdServico");

                    b.HasIndex("FkCategoria");

                    b.HasIndex("IdMarcacao");

                    b.ToTable("servicos");
                });

            modelBuilder.Entity("Karapinha.Model.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("EmailUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelemovelUsuario")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("TipoConta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsernameUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.HasOne("Karapinha.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("FkUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Karapinha.Model.Servico", b =>
                {
                    b.HasOne("Karapinha.Model.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("FkCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Karapinha.Model.Marcacao", "Marcacao")
                        .WithMany("Servicos")
                        .HasForeignKey("IdMarcacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Marcacao");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
