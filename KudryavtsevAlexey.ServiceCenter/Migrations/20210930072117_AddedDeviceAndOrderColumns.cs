using Microsoft.EntityFrameworkCore.Migrations;

namespace KudryavtsevAlexey.ServiceCenter.Migrations
{
    public partial class AddedDeviceAndOrderColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Devices",
                newName: "ProblemDescription");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProblemDescription",
                table: "Devices",
                newName: "Description");
        }
    }
}
