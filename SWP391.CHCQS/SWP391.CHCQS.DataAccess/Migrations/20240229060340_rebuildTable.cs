using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class rebuildTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IX_RejectedCustomQuotations_SubcriberId",
                table: "RejectedCustomQuotations");

            migrationBuilder.DropIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation");

            migrationBuilder.DropColumn(
                name: "SubcriberId",
                table: "RejectedCustomQuotations");

            migrationBuilder.AlterColumn<string>(
                name: "RejectedQuotationId",
                table: "RejectedCustomQuotations",
                type: "char(6)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldNullable: true);

           
            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation",
                column: "requestId",
                unique: true,
                filter: "[requestId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_CustomQuotation_RejectedQuotationId",
                table: "RejectedCustomQuotations",
                column: "RejectedQuotationId",
                principalTable: "CustomQuotation",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_EngineerId",
                table: "RejectedCustomQuotations",
                column: "EngineerId",
                principalTable: "Staff",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectedCustomQuotations_Staff_ManagerId",
                table: "RejectedCustomQuotations",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
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

            migrationBuilder.DropIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation");

            migrationBuilder.AlterColumn<string>(
                name: "RejectedQuotationId",
                table: "RejectedCustomQuotations",
                type: "char(6)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(6)");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)");

            migrationBuilder.AlterColumn<string>(
                name: "EngineerId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)");

            migrationBuilder.AddColumn<string>(
                name: "SubcriberId",
                table: "RejectedCustomQuotations",
                type: "char(5)",
                nullable: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_RejectedCustomQuotations_SubcriberId",
                table: "RejectedCustomQuotations",
                column: "SubcriberId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomQuotation_requestId",
                table: "CustomQuotation",
                column: "requestId");

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
    }
}
