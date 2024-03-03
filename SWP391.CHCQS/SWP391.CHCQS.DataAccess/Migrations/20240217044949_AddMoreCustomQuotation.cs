using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddMoreCustomQuotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                columns: new[] { "Date", "description" },
                values: new object[] { new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4815), "I want to build this house for my son and his wife, so i can live with them." });

            migrationBuilder.InsertData(
                table: "CustomQuotation",
                columns: new[] { "id", "acreage", "Date", "description", "engineerId", "location", "managerId", "requestId", "sellerId", "status", "total" },
                values: new object[,]
                {
                    { "CQ002", "340m2", new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4821), "This house must be great, so i can live with it for 500 years.", "EN001", "Quận 5, TP. Hồ Chí Minh", "MG001", "RF002", "SL001", 2, 0m },
                    { "CQ003", "740m2", new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4824), "This house for president to live, it must be nice.", "EN001", "Long Thạnh Mỹ, TP. Thủ Đức", "MG001", "RF003", "SL001", 2, 0m }
                });

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4386));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4412));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 11, 49, 48, 179, DateTimeKind.Local).AddTicks(4415));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002");

            migrationBuilder.DeleteData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                columns: new[] { "Date", "description" },
                values: new object[] { new DateTime(2024, 2, 17, 10, 28, 12, 687, DateTimeKind.Local).AddTicks(8000), "I want to build this house for my son and his wife, so i can live with them" });

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 10, 28, 12, 687, DateTimeKind.Local).AddTicks(7722));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 10, 28, 12, 687, DateTimeKind.Local).AddTicks(7732));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 10, 28, 12, 687, DateTimeKind.Local).AddTicks(7733));
        }
    }
}
