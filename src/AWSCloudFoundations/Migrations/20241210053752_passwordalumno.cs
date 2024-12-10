using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AWSCloudFoundations.Migrations
{
    /// <inheritdoc />
    public partial class passwordalumno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Alumnos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "Alumnos");
        }
    }
}
