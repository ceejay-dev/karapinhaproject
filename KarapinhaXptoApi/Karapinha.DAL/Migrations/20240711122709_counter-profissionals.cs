using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karapinha.DAL.Migrations
{
    /// <inheritdoc />
    public partial class counterprofissionals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Contator",
                table: "profissionais",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contator",
                table: "profissionais");
        }
    }
}
