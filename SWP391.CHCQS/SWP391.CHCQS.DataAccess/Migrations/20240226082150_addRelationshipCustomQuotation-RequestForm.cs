using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class addRelationshipCustomQuotationRequestForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation");


            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation",
                column: "requestId",
                unique: true,
                filter: "[requestId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4572));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4575));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4577));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4297));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 25, 13, 27, 13, 974, DateTimeKind.Local).AddTicks(4308));

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation",
                column: "requestId");
        }
    }
}
