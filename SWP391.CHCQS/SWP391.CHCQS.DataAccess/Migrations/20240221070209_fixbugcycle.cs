using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class fixbugcycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__engin__5629CD9C",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__manag__571DF1D5",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__reque__5812160E",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__CustomQuo__selle__59063A47",
                table: "CustomQuotation");

            migrationBuilder.DropForeignKey(
                name: "FK__Staff__managerId__628FA481",
                table: "Staff");

            migrationBuilder.AlterColumn<string>(
                name: "sellerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "requestId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "managerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "engineerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5);

            

            migrationBuilder.AddForeignKey(
                name: "FK_CustomQuotation_RequestForm_requestId",
                table: "CustomQuotation",
                column: "requestId",
                principalTable: "RequestForm",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomQuotation_Staff_engineerId",
                table: "CustomQuotation",
                column: "engineerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomQuotation_Staff_managerId",
                table: "CustomQuotation",
                column: "managerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomQuotation_Staff_sellerId",
                table: "CustomQuotation",
                column: "sellerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Staff_managerId",
                table: "Staff",
                column: "managerId",
                principalTable: "Staff",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomQuotation_RequestForm_requestId",
                table: "CustomQuotation");

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
                name: "sellerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "requestId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "managerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "engineerId",
                table: "CustomQuotation",
                type: "char(5)",
                unicode: false,
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldUnicode: false,
                oldFixedLength: true,
                oldMaxLength: 5,
                oldNullable: true);

          

            migrationBuilder.AddForeignKey(
                name: "FK__CustomQuo__engin__5629CD9C",
                table: "CustomQuotation",
                column: "engineerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__CustomQuo__manag__571DF1D5",
                table: "CustomQuotation",
                column: "managerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__CustomQuo__reque__5812160E",
                table: "CustomQuotation",
                column: "requestId",
                principalTable: "RequestForm",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__CustomQuo__selle__59063A47",
                table: "CustomQuotation",
                column: "sellerId",
                principalTable: "Staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK__Staff__managerId__628FA481",
                table: "Staff",
                column: "managerId",
                principalTable: "Staff",
                principalColumn: "id");
        }
    }
}
