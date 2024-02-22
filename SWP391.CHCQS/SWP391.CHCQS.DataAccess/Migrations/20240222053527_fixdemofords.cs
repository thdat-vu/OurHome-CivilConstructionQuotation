using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class fixdemofords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Construct__rooft__52593CB8",
                table: "ConstructDetail");

            

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructDetail_RooftopType_rooftopId",
                table: "ConstructDetail",
                column: "rooftopId",
                principalTable: "RooftopType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructDetail_RooftopType_rooftopId",
                table: "ConstructDetail");

            

            migrationBuilder.AddForeignKey(
                name: "FK__Construct__rooft__52593CB8",
                table: "ConstructDetail",
                column: "rooftopId",
                principalTable: "RooftopType",
                principalColumn: "id");
        }
    }
}
