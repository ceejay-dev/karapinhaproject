using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "horarios",
                columns: table => new
                {
                    IdHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_horarios", x => x.IdHorario);
                });

            migrationBuilder.CreateTable(
                name: "profissionais",
                columns: table => new
                {
                    IdProfissional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProfissional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FkCategoria = table.Column<int>(type: "int", nullable: false),
                    EmailProfissional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoProfissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BilheteProfissional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelemovelProfissional = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionais", x => x.IdProfissional);
                });

            migrationBuilder.CreateTable(
                name: "utilizadores",
                columns: table => new
                {
                    IdUtilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelemovelUtilizador = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    BilheteUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsernameUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordUtilizador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_utilizadores", x => x.IdUtilizador);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "servicos",
                columns: table => new
                {
                    IdServico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    FkCategoria = table.Column<int>(type: "int", nullable: false),
                    Contador = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "profissionalHorario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProfissional = table.Column<int>(type: "int", nullable: false),
                    IdHorario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profissionalHorario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profissionalHorario_horarios_IdHorario",
                        column: x => x.IdHorario,
                        principalTable: "horarios",
                        principalColumn: "IdHorario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profissionalHorario_profissionais_IdProfissional",
                        column: x => x.IdProfissional,
                        principalTable: "profissionais",
                        principalColumn: "IdProfissional",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "marcacoes",
                columns: table => new
                {
                    IdMarcacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecoMarcacao = table.Column<double>(type: "float", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataMarcacao = table.Column<DateOnly>(type: "date", nullable: false),
                    FkUtilizador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcacoes", x => x.IdMarcacao);
                    table.ForeignKey(
                        name: "FK_marcacoes_utilizadores_FkUtilizador",
                        column: x => x.FkUtilizador,
                        principalTable: "utilizadores",
                        principalColumn: "IdUtilizador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "marcacoesServicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkMarcacao = table.Column<int>(type: "int", nullable: false),
                    BookingIdMarcacao = table.Column<int>(type: "int", nullable: true),
                    FkServico = table.Column<int>(type: "int", nullable: false),
                    ServiceIdServico = table.Column<int>(type: "int", nullable: true),
                    FkProfissional = table.Column<int>(type: "int", nullable: false),
                    ProfissionalIdProfissional = table.Column<int>(type: "int", nullable: true),
                    HoraMarcacao = table.Column<int>(type: "int", nullable: false),
                    HorarioIdHorario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcacoesServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_marcacoesServicos_horarios_HorarioIdHorario",
                        column: x => x.HorarioIdHorario,
                        principalTable: "horarios",
                        principalColumn: "IdHorario");
                    table.ForeignKey(
                        name: "FK_marcacoesServicos_marcacoes_BookingIdMarcacao",
                        column: x => x.BookingIdMarcacao,
                        principalTable: "marcacoes",
                        principalColumn: "IdMarcacao");
                    table.ForeignKey(
                        name: "FK_marcacoesServicos_profissionais_ProfissionalIdProfissional",
                        column: x => x.ProfissionalIdProfissional,
                        principalTable: "profissionais",
                        principalColumn: "IdProfissional");
                    table.ForeignKey(
                        name: "FK_marcacoesServicos_servicos_ServiceIdServico",
                        column: x => x.ServiceIdServico,
                        principalTable: "servicos",
                        principalColumn: "IdServico");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_marcacoes_FkUtilizador",
                table: "marcacoes",
                column: "FkUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_marcacoesServicos_BookingIdMarcacao",
                table: "marcacoesServicos",
                column: "BookingIdMarcacao");

            migrationBuilder.CreateIndex(
                name: "IX_marcacoesServicos_HorarioIdHorario",
                table: "marcacoesServicos",
                column: "HorarioIdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_marcacoesServicos_ProfissionalIdProfissional",
                table: "marcacoesServicos",
                column: "ProfissionalIdProfissional");

            migrationBuilder.CreateIndex(
                name: "IX_marcacoesServicos_ServiceIdServico",
                table: "marcacoesServicos",
                column: "ServiceIdServico");

            migrationBuilder.CreateIndex(
                name: "IX_profissionalHorario_IdHorario",
                table: "profissionalHorario",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_profissionalHorario_IdProfissional",
                table: "profissionalHorario",
                column: "IdProfissional");

            migrationBuilder.CreateIndex(
                name: "IX_servicos_FkCategoria",
                table: "servicos",
                column: "FkCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "marcacoesServicos");

            migrationBuilder.DropTable(
                name: "profissionalHorario");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "marcacoes");

            migrationBuilder.DropTable(
                name: "servicos");

            migrationBuilder.DropTable(
                name: "horarios");

            migrationBuilder.DropTable(
                name: "profissionais");

            migrationBuilder.DropTable(
                name: "utilizadores");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
