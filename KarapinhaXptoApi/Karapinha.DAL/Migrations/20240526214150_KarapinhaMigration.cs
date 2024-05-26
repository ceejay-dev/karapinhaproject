using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class KarapinhaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    IdProfissional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkServico = table.Column<int>(type: "int", nullable: false),
                    EmailProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BilheteProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelemovelProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    horarios = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.IdProfissional);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelemovelUsuario = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    FotoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsernameUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoConta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "marcacoes",
                columns: table => new
                {
                    IdMarcacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataMarcacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraMarcacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FkUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcacoes", x => x.IdMarcacao);
                    table.ForeignKey(
                        name: "FK_marcacoes_usuarios_FkUsuario",
                        column: x => x.FkUsuario,
                        principalTable: "usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servicos",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeServico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    FkCategoria = table.Column<int>(type: "int", nullable: false),
                    IdMarcacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicos", x => x.IdServico);
                    table.ForeignKey(
                        name: "FK_servicos_categorias_FkCategoria",
                        column: x => x.FkCategoria,
                        principalTable: "categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_servicos_marcacoes_IdMarcacao",
                        column: x => x.IdMarcacao,
                        principalTable: "marcacoes",
                        principalColumn: "IdMarcacao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_marcacoes_FkUsuario",
                table: "marcacoes",
                column: "FkUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_FkCategoria",
                table: "servicos",
                column: "FkCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_IdMarcacao",
                table: "servicos",
                column: "IdMarcacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profissionais");

            migrationBuilder.DropTable(
                name: "servicos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "marcacoes");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
