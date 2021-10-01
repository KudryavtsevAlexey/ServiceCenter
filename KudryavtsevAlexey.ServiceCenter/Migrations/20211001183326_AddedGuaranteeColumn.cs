using Microsoft.EntityFrameworkCore.Migrations;

namespace KudryavtsevAlexey.ServiceCenter.Migrations
{
    public partial class AddedGuaranteeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OnGuarantee",
                table: "Devices",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnGuarantee",
                table: "Devices");
        }
    }
}
