using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class profissionalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfissionalHorarios_horarios_FkHorario",
                table: "ProfissionalHorarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfissionalHorarios_profissionais_FkProfissional",
                table: "ProfissionalHorarios");

            migrationBuilder.DropColumn(
                name: "HoraMarcacao",
                table: "marcacoes");

            migrationBuilder.RenameColumn(
                name: "FkProfissional",
                table: "ProfissionalHorarios",
                newName: "IdProfissional");

            migrationBuilder.RenameColumn(
                name: "FkHorario",
                table: "ProfissionalHorarios",
                newName: "IdHorario");

            migrationBuilder.RenameIndex(
                name: "IX_ProfissionalHorarios_FkProfissional",
                table: "ProfissionalHorarios",
                newName: "IX_ProfissionalHorarios_IdProfissional");

            migrationBuilder.RenameIndex(
                name: "IX_ProfissionalHorarios_FkHorario",
                table: "ProfissionalHorarios",
                newName: "IX_ProfissionalHorarios_IdHorario");

            migrationBuilder.AddColumn<int>(
                name: "BookingIdMarcacao",
                table: "MarcacoesServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryIdCategoria",
                table: "MarcacoesServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceIdServico",
                table: "MarcacoesServicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FkHorario",
                table: "marcacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MarcacoesServicos_BookingIdMarcacao",
                table: "MarcacoesServicos",
                column: "BookingIdMarcacao");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacoesServicos_CategoryIdCategoria",
                table: "MarcacoesServicos",
                column: "CategoryIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacoesServicos_ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                column: "ProfissionalsIdProfissional");

            migrationBuilder.CreateIndex(
                name: "IX_MarcacoesServicos_ServiceIdServico",
                table: "MarcacoesServicos",
                column: "ServiceIdServico");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_categorias_CategoryIdCategoria",
                table: "MarcacoesServicos",
                column: "CategoryIdCategoria",
                principalTable: "categorias",
                principalColumn: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_marcacoes_BookingIdMarcacao",
                table: "MarcacoesServicos",
                column: "BookingIdMarcacao",
                principalTable: "marcacoes",
                principalColumn: "IdMarcacao");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalsIdProfissional",
                table: "MarcacoesServicos",
                column: "ProfissionalsIdProfissional",
                principalTable: "profissionais",
                principalColumn: "IdProfissional");

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_servicos_ServiceIdServico",
                table: "MarcacoesServicos",
                column: "ServiceIdServico",
                principalTable: "servicos",
                principalColumn: "IdServico");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfissionalHorarios_horarios_IdHorario",
                table: "ProfissionalHorarios",
                column: "IdHorario",
                principalTable: "horarios",
                principalColumn: "IdHorario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfissionalHorarios_profissionais_IdProfissional",
                table: "ProfissionalHorarios",
                column: "IdProfissional",
                principalTable: "profissionais",
                principalColumn: "IdProfissional",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_categorias_CategoryIdCategoria",
                table: "MarcacoesServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_marcacoes_BookingIdMarcacao",
                table: "MarcacoesServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_profissionais_ProfissionalsIdProfissional",
                table: "MarcacoesServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_servicos_ServiceIdServico",
                table: "MarcacoesServicos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfissionalHorarios_horarios_IdHorario",
                table: "ProfissionalHorarios");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfissionalHorarios_profissionais_IdProfissional",
                table: "ProfissionalHorarios");

            migrationBuilder.DropIndex(
                name: "IX_MarcacoesServicos_BookingIdMarcacao",
                table: "MarcacoesServicos");

            migrationBuilder.DropIndex(
                name: "IX_MarcacoesServicos_CategoryIdCategoria",
                table: "MarcacoesServicos");

            migrationBuilder.DropIndex(
                name: "IX_MarcacoesServicos_ProfissionalsIdProfissional",
                table: "MarcacoesServicos");

            migrationBuilder.DropIndex(
                name: "IX_MarcacoesServicos_ServiceIdServico",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "BookingIdMarcacao",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "CategoryIdCategoria",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "ProfissionalsIdProfissional",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "ServiceIdServico",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "FkHorario",
                table: "marcacoes");

            migrationBuilder.RenameColumn(
                name: "IdProfissional",
                table: "ProfissionalHorarios",
                newName: "FkProfissional");

            migrationBuilder.RenameColumn(
                name: "IdHorario",
                table: "ProfissionalHorarios",
                newName: "FkHorario");

            migrationBuilder.RenameIndex(
                name: "IX_ProfissionalHorarios_IdProfissional",
                table: "ProfissionalHorarios",
                newName: "IX_ProfissionalHorarios_FkProfissional");

            migrationBuilder.RenameIndex(
                name: "IX_ProfissionalHorarios_IdHorario",
                table: "ProfissionalHorarios",
                newName: "IX_ProfissionalHorarios_FkHorario");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraMarcacao",
                table: "marcacoes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_ProfissionalHorarios_horarios_FkHorario",
                table: "ProfissionalHorarios",
                column: "FkHorario",
                principalTable: "horarios",
                principalColumn: "IdHorario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfissionalHorarios_profissionais_FkProfissional",
                table: "ProfissionalHorarios",
                column: "FkProfissional",
                principalTable: "profissionais",
                principalColumn: "IdProfissional",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
