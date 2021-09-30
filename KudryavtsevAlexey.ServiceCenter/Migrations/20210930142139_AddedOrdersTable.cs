using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KudryavtsevAlexey.ServiceCenter.Migrations
{
    public partial class AddedOrdersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Clients_ClientId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Masters_MasterId",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    AmountToPay = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "MasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeviceId",
                table: "Orders",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MasterId",
                table: "Orders",
                column: "MasterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Clients_ClientId",
                table: "Devices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Masters_MasterId",
                table: "Devices",
                column: "MasterId",
                principalTable: "Masters",
                principalColumn: "MasterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Clients_ClientId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Masters_MasterId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Clients_ClientId",
                table: "Devices",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Masters_MasterId",
                table: "Devices",
                column: "MasterId",
                principalTable: "Masters",
                principalColumn: "MasterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
