using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class RebuiltRejectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotaionTask_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "CustomQuotaionTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDetail_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "MaterialDetail");

            migrationBuilder.DropIndex(
                name: "IX_MaterialDetail_RejectedCustomQuotationId",
                table: "MaterialDetail");

            migrationBuilder.DropIndex(
                name: "IX_CustomQuotaionTask_RejectedCustomQuotationId",
                table: "CustomQuotaionTask");

            migrationBuilder.DropColumn(
                name: "RejectedCustomQuotationId",
                table: "MaterialDetail");

            migrationBuilder.DropColumn(
                name: "RejectedCustomQuotationId",
                table: "CustomQuotaionTask");

            migrationBuilder.AlterColumn<string>(
                name: "RejectedQuotationId",
                table: "RejectedCustomQuotations",
                type: "char(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EngineerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RejectedCustomQuotations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SubcriberId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCustomQuotations_EngineerId",
                table: "RejectedCustomQuotations",
                column: "EngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCustomQuotations_ManagerId",
                table: "RejectedCustomQuotations",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCustomQuotations_RejectedQuotationId",
                table: "RejectedCustomQuotations",
                column: "RejectedQuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCustomQuotations_SubcriberId",
                table: "RejectedCustomQuotations",
                column: "SubcriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_CustomQuotation_RejectedQuotationId",
                table: "RejectedCustomQuotations",
                column: "RejectedQuotationId",
                principalTable: "CustomQuotation",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_EngineerId",
                table: "RejectedCustomQuotations",
                column: "EngineerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_ManagerId",
                table: "RejectedCustomQuotations",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_SubcriberId",
                table: "RejectedCustomQuotations",
                column: "SubcriberId",
                principalTable: "Staff",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RejectedCustomQuotations_CustomQuotation_RejectedQuotationId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_EngineerId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_ManagerId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_SubcriberId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropIndex(
                name: "IX_RejectedCustomQuotations_EngineerId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropIndex(
                name: "IX_RejectedCustomQuotations_ManagerId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropIndex(
                name: "IX_RejectedCustomQuotations_RejectedQuotationId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropIndex(
                name: "IX_RejectedCustomQuotations_SubcriberId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropColumn(
                name: "SubcriberId",
                table: "RejectedCustomQuotations");

            migrationBuilder.AlterColumn<string>(
                name: "RejectedQuotationId",
                table: "RejectedCustomQuotations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "RejectedCustomQuotations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineerId",
                table: "RejectedCustomQuotations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedCustomQuotationId",
                table: "MaterialDetail",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedCustomQuotationId",
                table: "CustomQuotaionTask",
                type: "nvarchar(450)",
                nullable: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDetail_RejectedCustomQuotationId",
                table: "MaterialDetail",
                column: "RejectedCustomQuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotaionTask_RejectedCustomQuotationId",
                table: "CustomQuotaionTask",
                column: "RejectedCustomQuotationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomQuotaionTask_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "CustomQuotaionTask",
                column: "RejectedCustomQuotationId",
                principalTable: "RejectedCustomQuotations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialDetail_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "MaterialDetail",
                column: "RejectedCustomQuotationId",
                principalTable: "RejectedCustomQuotations",
                principalColumn: "Id");
        }
    }
}
