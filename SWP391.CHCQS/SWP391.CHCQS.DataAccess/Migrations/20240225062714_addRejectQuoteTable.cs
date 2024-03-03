using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class addRejectQuoteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "RejectedCustomQuotations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RejectedQuotationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectedCustomQuotations", x => x.Id);
                });

           

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotaionTask_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "CustomQuotaionTask");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialDetail_RejectedCustomQuotations_RejectedCustomQuotationId",
                table: "MaterialDetail");

            migrationBuilder.DropTable(
                name: "RejectedCustomQuotations");

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

           
        }
    }
}
