using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class RemoveIcollect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Construct__basem__4D94879B",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__Construct__const__4E88ABD4",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__Construct__found__4D94879B",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__Construct__inves__5070F446",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__StandardQ__const__6477ECF3",
                table: "StandardQuotation");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructDetail_BasementType_basementId",
                table: "ConstructDetail",
                column: "basementId",
                principalTable: "BasementType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructDetail_ConstructionType_constructionId",
                table: "ConstructDetail",
                column: "constructionId",
                principalTable: "ConstructionType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructDetail_FoundationType_foundationId",
                table: "ConstructDetail",
                column: "foundationId",
                principalTable: "FoundationType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructDetail_InvestmentType_investmentId",
                table: "ConstructDetail",
                column: "investmentId",
                principalTable: "InvestmentType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StandardQuotation_ConstructionType_constructionId",
                table: "StandardQuotation",
                column: "constructionId",
                principalTable: "ConstructionType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructDetail_BasementType_basementId",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructDetail_ConstructionType_constructionId",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructDetail_FoundationType_foundationId",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ConstructDetail_InvestmentType_investmentId",
                table: "ConstructDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StandardQuotation_ConstructionType_constructionId",
                table: "StandardQuotation");

            migrationBuilder.AddForeignKey(
                name: "FK__Construct__basem__4D94879B",
                table: "ConstructDetail",
                column: "basementId",
                principalTable: "BasementType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Construct__const__4E88ABD4",
                table: "ConstructDetail",
                column: "constructionId",
                principalTable: "ConstructionType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Construct__found__4D94879B",
                table: "ConstructDetail",
                column: "foundationId",
                principalTable: "FoundationType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Construct__inves__5070F446",
                table: "ConstructDetail",
                column: "investmentId",
                principalTable: "InvestmentType",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__StandardQ__const__6477ECF3",
                table: "StandardQuotation",
                column: "constructionId",
                principalTable: "ConstructionType",
                principalColumn: "id");
        }
    }
}
