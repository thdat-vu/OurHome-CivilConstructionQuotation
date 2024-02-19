using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddPropertiesAndRejectedCustomQuotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptanceDateManager",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DelegationDateSeller",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecieveDateEngineer",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecieveDateManager",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDateEngineer",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDateSeller",
                table: "CustomQuotation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1797));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1800));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1803));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1479));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1488));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 19, 17, 38, 28, 741, DateTimeKind.Local).AddTicks(1490));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptanceDateManager",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "DelegationDateSeller",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "RecieveDateEngineer",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "RecieveDateManager",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "SubmissionDateEngineer",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "SubmissionDateSeller",
                table: "CustomQuotation");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3808));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3813));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3815));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3373));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3395));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 17, 21, 39, 40, 470, DateTimeKind.Local).AddTicks(3397));
        }
    }
}
