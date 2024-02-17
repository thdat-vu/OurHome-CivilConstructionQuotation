using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class ModifyCustomQuotationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "CustomQuotation",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                columns: new[] { "Date", "status" },
                values: new object[] { new DateTime(2024, 2, 17, 10, 28, 12, 687, DateTimeKind.Local).AddTicks(8000), 2 });
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "CustomQuotation",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                columns: new[] { "Date", "status" },
                values: new object[] { new DateTime(2024, 2, 16, 20, 6, 0, 884, DateTimeKind.Local).AddTicks(3320), true });            
        }
    }
}
