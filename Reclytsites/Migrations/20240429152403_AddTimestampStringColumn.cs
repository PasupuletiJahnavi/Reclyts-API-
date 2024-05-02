using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclytsites.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampStringColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimestampString",
                table: "UpdateInfos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimestampString",
                table: "UpdateInfos");
        }
    }
}
