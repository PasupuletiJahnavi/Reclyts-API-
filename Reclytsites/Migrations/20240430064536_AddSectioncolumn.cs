using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclytsites.Migrations
{
    /// <inheritdoc />
    public partial class AddSectioncolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Comments");
        }
    }
}
