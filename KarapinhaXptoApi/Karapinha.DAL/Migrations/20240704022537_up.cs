using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalsIdProfissional",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "FkHorario",
                table: "marcacoes");

            migrationBuilder.RenameColumn(
                name: "ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                newName: "ProfissionalIdProfissional");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacoesServicos_ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                newName: "IX_MarcacoesServicos_ProfissionalIdProfissional");

            migrationBuilder.AddColumn<int>(
                name: "Contador",
                table: "servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraMarcacao",
                table: "marcacoes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalIdProfissional",
                table: "MarcacoesServicos",
                column: "ProfissionalIdProfissional",
                principalTable: "profissionais",
                principalColumn: "IdProfissional");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalIdProfissional",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "Contador",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "HoraMarcacao",
                table: "marcacoes");

            migrationBuilder.RenameColumn(
                name: "ProfissionalIdProfissional",
                table: "MarcacoesServicos",
                newName: "ProfissionalsIdProfissional");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacoesServicos_ProfissionalIdProfissional",
                table: "MarcacoesServicos",
                newName: "IX_MarcacoesServicos_ProfissionalsIdProfissional");

            migrationBuilder.AddColumn<int>(
                name: "FkHorario",
                table: "marcacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                column: "ProfissionalsIdProfissional",
                principalTable: "profissionais",
                principalColumn: "IdProfissional");
        }
    }
}
