using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddOverViewDateDataToProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Project",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 17, 20, 4, 6, 818, DateTimeKind.Local).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ01",
                column: "Date",
                value: new DateTime(2021, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ02",
                column: "Date",
                value: new DateTime(2021, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ03",
                column: "Date",
                value: new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ04",
                column: "Date",
                value: new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ05",
                column: "Date",
                value: new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 20, 4, 6, 818, DateTimeKind.Local).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 20, 4, 6, 818, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 20, 4, 6, 818, DateTimeKind.Local).AddTicks(6839));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Project");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 17, 19, 23, 33, 493, DateTimeKind.Local).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 19, 23, 33, 493, DateTimeKind.Local).AddTicks(6962));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 19, 23, 33, 493, DateTimeKind.Local).AddTicks(6973));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 19, 23, 33, 493, DateTimeKind.Local).AddTicks(6975));
        }
    }
}
