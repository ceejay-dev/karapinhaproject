using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateinBookingsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_categorias_CategoryIdCategoria",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "DataMarcacao",
                table: "marcacoes");

            migrationBuilder.DropColumn(
                name: "HoraMarcacao",
                table: "marcacoes");

            migrationBuilder.RenameColumn(
                name: "FkCategoria",
                table: "MarcacoesServicos",
                newName: "HoraMarcacao");

            migrationBuilder.RenameColumn(
                name: "CategoryIdCategoria",
                table: "MarcacoesServicos",
                newName: "HorarioIdHorario");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacoesServicos_CategoryIdCategoria",
                table: "MarcacoesServicos",
                newName: "IX_MarcacoesServicos_HorarioIdHorario");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataMarcacao",
                table: "MarcacoesServicos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_horarios_HorarioIdHorario",
                table: "MarcacoesServicos",
                column: "HorarioIdHorario",
                principalTable: "horarios",
                principalColumn: "IdHorario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarcacoesServicos_horarios_HorarioIdHorario",
                table: "MarcacoesServicos");

            migrationBuilder.DropColumn(
                name: "DataMarcacao",
                table: "MarcacoesServicos");

            migrationBuilder.RenameColumn(
                name: "HorarioIdHorario",
                table: "MarcacoesServicos",
                newName: "CategoryIdCategoria");

            migrationBuilder.RenameColumn(
                name: "HoraMarcacao",
                table: "MarcacoesServicos",
                newName: "FkCategoria");

            migrationBuilder.RenameIndex(
                name: "IX_MarcacoesServicos_HorarioIdHorario",
                table: "MarcacoesServicos",
                newName: "IX_MarcacoesServicos_CategoryIdCategoria");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataMarcacao",
                table: "marcacoes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraMarcacao",
                table: "marcacoes",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_MarcacoesServicos_categorias_CategoryIdCategoria",
                table: "MarcacoesServicos",
                column: "CategoryIdCategoria",
                principalTable: "categorias",
                principalColumn: "IdCategoria");
        }
    }
}
