using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class fixCycleRequestCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__RequestFo__custo__5FB337D6",
                table: "RequestForm");

           

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForm_Customer_customerId",
                table: "RequestForm",
                column: "customerId",
                principalTable: "Customer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForm_Customer_customerId",
                table: "RequestForm");

            

            migrationBuilder.AddForeignKey(
                name: "FK__RequestFo__custo__5FB337D6",
                table: "RequestForm",
                column: "customerId",
                principalTable: "Customer",
                principalColumn: "id");
        }
    }
}
