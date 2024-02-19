using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddDataForConstructDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConstructDetail",
                columns: new[] { "quotationId", "alley", "balcony", "basementId", "constructionId", "facade", "floor", "foundationId", "garden", "investmentId", "length", "mezzanine", "rooftopFloor", "rooftopId", "room", "width" },
                values: new object[] { "CQ001", "3m", true, "BT1", "CT1", 1, 2, "FT1", 20m, "IT1", 200m, 30m, 40m, "RT1", 5, 100m });

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 16, 20, 6, 0, 884, DateTimeKind.Local).AddTicks(3320));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 20, 6, 0, 884, DateTimeKind.Local).AddTicks(3004));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 20, 6, 0, 884, DateTimeKind.Local).AddTicks(3021));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 20, 6, 0, 884, DateTimeKind.Local).AddTicks(3024));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConstructDetail",
                keyColumn: "quotationId",
                keyValue: "CQ001");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1871));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1837));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1850));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1853));
        }
    }
}
