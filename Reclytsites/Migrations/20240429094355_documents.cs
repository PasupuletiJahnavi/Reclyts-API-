using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclytsites.Migrations
{
    /// <inheritdoc />
    public partial class documents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DownloadUrl",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ViewUrl",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadUrl",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ViewUrl",
                table: "Documents");
        }
    }
}
