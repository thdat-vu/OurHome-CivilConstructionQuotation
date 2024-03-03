using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddMoreConstrucDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConstructDetail",
                columns: new[] { "quotationId", "alley", "balcony", "basementId", "constructionId", "facade", "floor", "foundationId", "garden", "investmentId", "length", "mezzanine", "rooftopFloor", "rooftopId", "room", "width" },
                values: new object[,]
                {
                    { "CQ002", "3m", true, "BT2", "CT2", 1, 2, "FT2", 20m, "IT2", 200m, 30m, 40m, "RT2", 5, 100m },
                    { "CQ003", "3m", true, "BT3", "CT3", 1, 2, "FT3", 20m, "IT3", 200m, 30m, 40m, "RT3", 5, 100m }
                });

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(4084));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(3846));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 22, 11, 10, 35, 409, DateTimeKind.Local).AddTicks(3849));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConstructDetail",
                keyColumn: "quotationId",
                keyValue: "CQ002");

            migrationBuilder.DeleteData(
                table: "ConstructDetail",
                keyColumn: "quotationId",
                keyValue: "CQ003");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(822));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(450));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 21, 14, 2, 8, 323, DateTimeKind.Local).AddTicks(453));
        }
    }
}
