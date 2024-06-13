using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class correcaomarcacaoservico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_servicos_marcacoes_IdMarcacao",
                table: "servicos");

            migrationBuilder.DropIndex(
                name: "IX_servicos_IdMarcacao",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "IdMarcacao",
                table: "servicos");

            migrationBuilder.DropColumn(
                name: "Marcacoes",
                table: "marcacoes");

            migrationBuilder.AddColumn<double>(
                name: "PrecoMarcacao",
                table: "marcacoes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoMarcacao",
                table: "marcacoes");

            migrationBuilder.AddColumn<int>(
                name: "IdMarcacao",
                table: "servicos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Marcacoes",
                table: "marcacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_servicos_IdMarcacao",
                table: "servicos",
                column: "IdMarcacao");

            migrationBuilder.AddForeignKey(
                name: "FK_servicos_marcacoes_IdMarcacao",
                table: "servicos",
                column: "IdMarcacao",
                principalTable: "marcacoes",
                principalColumn: "IdMarcacao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
