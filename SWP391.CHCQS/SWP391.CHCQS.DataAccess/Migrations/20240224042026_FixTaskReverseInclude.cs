using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class FixTaskReverseInclude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__taskI__534D60F1",
                table: "CustomQuotaionTask");

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
                name: "FK_CustomQuotaionTask_Task_taskId",
                table: "CustomQuotaionTask",
                column: "taskId",
                principalTable: "Task",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotaionTask_Task_taskId",
                table: "CustomQuotaionTask");

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ001",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 339, DateTimeKind.Local).AddTicks(54));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ002",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 339, DateTimeKind.Local).AddTicks(59));

            //migrationBuilder.UpdateData(
            //    table: "CustomQuotation",
            //    keyColumn: "id",
            //    keyValue: "CQ003",
            //    column: "Date",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 339, DateTimeKind.Local).AddTicks(63));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF001",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 338, DateTimeKind.Local).AddTicks(9771));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF002",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 338, DateTimeKind.Local).AddTicks(9783));

            //migrationBuilder.UpdateData(
            //    table: "RequestForm",
            //    keyColumn: "id",
            //    keyValue: "RF003",
            //    column: "generateDate",
            //    value: new DateTime(2024, 2, 22, 12, 44, 7, 338, DateTimeKind.Local).AddTicks(9786));

            migrationBuilder.AddForeignKey(
                name: "FK__CustomQuo__taskI__534D60F1",
                table: "CustomQuotaionTask",
                column: "taskId",
                principalTable: "Task",
                principalColumn: "id");
        }
    }
}
