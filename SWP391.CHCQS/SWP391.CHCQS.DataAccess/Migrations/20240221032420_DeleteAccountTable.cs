using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class DeleteAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Customer__userna__534D60F1",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__engin__5629CD9C",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__manag__571DF1D5",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__selle__59063A47",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__Staff__managerId__628FA481",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK__Staff__username__6383C8BA",
                table: "Staff");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Staff_username",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Customer_username",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Customer",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20);


            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotation_Staff_engineerId",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotation_Staff_managerId",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotation_Staff_sellerId",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Staff_managerId",
                table: "Staff");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Customer",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldUnicode: false,
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__F3DBC5731230846A", x => x.username);
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "username", "password", "role" },
                values: new object[,]
                {
                    { "anhnth", "1", "admin" },
                    { "bthuong", "1", "customer" },
                    { "datnt", "1", "engineer" },
                    { "datnx", "1", "manager" },
                    { "dtuan", "1", "customer" },
                    { "duclm", "1", "seller" },
                    { "hoanguyen", "1", "customer" },
                    { "lanly22", "1", "customer" },
                    { "lvm123", "1", "customer" },
                    { "maitran1", "1", "customer" },
                    { "ngocanh85", "1", "customer" },
                    { "phai789", "1", "customer" },
                    { "thao123", "1", "customer" },
                    { "vnam", "1", "customer" }
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_Staff_username",
                table: "Staff",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_username",
                table: "Customer",
                column: "username");

           
        }
    }
}
