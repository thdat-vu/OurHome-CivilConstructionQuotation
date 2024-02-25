using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class DeleteTaskCategoryAndTaskCycleCategoryProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Material__catego__5812160E",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK__Task__categoryId__656C112C",
                table: "Task");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1701));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1416));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1426));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 16, 59, 5, 82, DateTimeKind.Local).AddTicks(1428));

            migrationBuilder.AddForeignKey(
                name: "FK_Material_MaterialCategory_categoryId",
                table: "Material",
                column: "categoryId",
                principalTable: "MaterialCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TaskCategory_categoryId",
                table: "Task",
                column: "categoryId",
                principalTable: "TaskCategory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_MaterialCategory_categoryId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_TaskCategory_categoryId",
                table: "Task");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ002",
                column: "Date",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2956));

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ003",
                column: "Date",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2960));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2695));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2710));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003",
                column: "generateDate",
                value: new DateTime(2024, 2, 24, 12, 48, 3, 565, DateTimeKind.Local).AddTicks(2713));

            migrationBuilder.AddForeignKey(
                name: "FK__Material__catego__5812160E",
                table: "Material",
                column: "categoryId",
                principalTable: "MaterialCategory",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Task__categoryId__656C112C",
                table: "Task",
                column: "categoryId",
                principalTable: "TaskCategory",
                principalColumn: "id");
        }
    }
}
