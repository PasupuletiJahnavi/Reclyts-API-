using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclytsites.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Projects");
        }
    }
}
