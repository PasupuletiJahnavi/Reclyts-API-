using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reclytsites.Migrations
{
    /// <inheritdoc />
    public partial class CreateTasksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EssenceOfAssessment",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TravelDistance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoursSpent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecallType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDevices = table.Column<int>(type: "int", nullable: false),
                    SerialNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDesignation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "EssenceOfAssessment",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
