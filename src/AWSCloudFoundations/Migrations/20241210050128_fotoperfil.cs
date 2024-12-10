using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AWSCloudFoundations.Migrations
{
    /// <inheritdoc />
    public partial class fotoperfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fotoPerfilUrl",
                table: "Alumnos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fotoPerfilUrl",
                table: "Alumnos");
        }
    }
}
