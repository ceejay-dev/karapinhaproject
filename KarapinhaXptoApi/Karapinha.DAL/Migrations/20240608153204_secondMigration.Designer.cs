﻿// <auto-generated />
using System;
using Karapinha.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    [DbContext(typeof(KarapinhaDbContext))]
    [Migration("20240608153204_secondMigration")]
    partial class secondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCategoria");

                    b.ToTable("categorias");
                });

            modelBuilder.Entity("Karapinha.Model.Horario", b =>
                {
                    b.Property<int>("IdHorario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHorario"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHorario");

                    b.ToTable("horarios");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.Property<int>("IdMarcacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMarcacao"));

                    b.Property<DateOnly>("DataMarcacao")
                        .HasColumnType("date");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkUtilizador")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("HoraMarcacao")
                        .HasColumnType("time");

                    b.Property<int>("Marcacoes")
                        .HasColumnType("int");

                    b.HasKey("IdMarcacao");

                    b.HasIndex("FkUtilizador");

                    b.ToTable("marcacoes");
                });

            modelBuilder.Entity("Karapinha.Model.Profissional", b =>
                {
                    b.Property<int>("IdProfissional")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfissional"));

                    b.Property<string>("BilheteProfissional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailProfissional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FkCategoria")
                        .HasColumnType("int");

                    b.Property<string>("FotoProfissional")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeProfissional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelemovelProfissional")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProfissional");

                    b.ToTable("profissionais");
                });

            modelBuilder.Entity("Karapinha.Model.ProfissionalHorario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FkHorario")
                        .HasColumnType("int");

                    b.Property<int>("FkProfissional")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FkHorario");

                    b.HasIndex("FkProfissional");

                    b.ToTable("ProfissionalHorarios");
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("IdServico");

                    b.HasIndex("FkCategoria");

                    b.HasIndex("IdMarcacao");

                    b.ToTable("servicos");
                });

            modelBuilder.Entity("Karapinha.Model.Utilizador", b =>
                {
                    b.Property<int>("IdUtilizador")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUtilizador"));

                    b.Property<string>("EmailUtilizador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoUtilizador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeUtilizador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordUtilizador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelemovelUtilizador")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("TipoPerfil")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsernameUtilizador")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUtilizador");

                    b.ToTable("utilizadores");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.HasOne("Karapinha.Model.Utilizador", "Utilizador")
                        .WithMany()
                        .HasForeignKey("FkUtilizador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("Karapinha.Model.ProfissionalHorario", b =>
                {
                    b.HasOne("Karapinha.Model.Horario", "Horario")
                        .WithMany()
                        .HasForeignKey("FkHorario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Karapinha.Model.Profissional", "Profissional")
                        .WithMany()
                        .HasForeignKey("FkProfissional")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Horario");

                    b.Navigation("Profissional");
                });

            modelBuilder.Entity("Karapinha.Model.Servico", b =>
                {
                    b.HasOne("Karapinha.Model.Categoria", "Categoria")
                        .WithMany("Servicos")
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

            modelBuilder.Entity("Karapinha.Model.Categoria", b =>
                {
                    b.Navigation("Servicos");
                });

            modelBuilder.Entity("Karapinha.Model.Marcacao", b =>
                {
                    b.Navigation("Servicos");
                });
#pragma warning restore 612, 618
        }
    }
}
