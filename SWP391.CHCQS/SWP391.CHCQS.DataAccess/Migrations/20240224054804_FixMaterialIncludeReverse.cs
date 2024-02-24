using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class FixMaterialIncludeReverse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__MaterialD__mater__59063A47",
                table: "MaterialDetail");

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ001",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2952));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ002",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2956));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ003",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2960));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF001",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2695));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF002",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2710));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF003",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialDetail_Material_materialId",
                table: "MaterialDetail",
                column: "materialId",
                principalTable: "Material",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDetail_Material_materialId",
                table: "MaterialDetail");

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ001",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9828));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ002",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9836));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ003",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9839));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF001",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9102));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF002",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9126));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF003",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 24, 11, 20, 25, 587, DateTimeKind.Local).AddTicks(9129));

            migrationBuilder.AddForeignKey(
                name: "FK__MaterialD__mater__59063A47",
                table: "MaterialDetail",
                column: "materialId",
                principalTable: "Material",
                principalColumn: "id");
        }
    }
}
