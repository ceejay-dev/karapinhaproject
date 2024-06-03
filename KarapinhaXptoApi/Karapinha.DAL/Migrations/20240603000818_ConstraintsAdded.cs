using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConstraintsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_marcacoes_Utilizadores_FkUtilizador",
                table: "marcacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores");

            migrationBuilder.RenameTable(
                name: "Utilizadores",
                newName: "utilizadores");

            migrationBuilder.AlterColumn<string>(
                name: "NomeServico",
                table: "servicos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TelemovelProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilheteProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "HoraMarcacao",
                table: "marcacoes",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataMarcacao",
                table: "marcacoes",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "horarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_utilizadores",
                table: "utilizadores",
                column: "IdUtilizador");

            migrationBuilder.AddForeignKey(
                name: "FK_marcacoes_utilizadores_FkUtilizador",
                table: "marcacoes",
                column: "FkUtilizador",
                principalTable: "utilizadores",
                principalColumn: "IdUtilizador",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_marcacoes_utilizadores_FkUtilizador",
                table: "marcacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_utilizadores",
                table: "utilizadores");

            migrationBuilder.RenameTable(
                name: "utilizadores",
                newName: "Utilizadores");

            migrationBuilder.AlterColumn<string>(
                name: "NomeServico",
                table: "servicos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TelemovelProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmailProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BilheteProfissional",
                table: "profissionais",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraMarcacao",
                table: "marcacoes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataMarcacao",
                table: "marcacoes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "horarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeCategoria",
                table: "categorias",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilizadores",
                table: "Utilizadores",
                column: "IdUtilizador");

            migrationBuilder.AddForeignKey(
                name: "FK_marcacoes_Utilizadores_FkUtilizador",
                table: "marcacoes",
                column: "FkUtilizador",
                principalTable: "Utilizadores",
                principalColumn: "IdUtilizador",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
