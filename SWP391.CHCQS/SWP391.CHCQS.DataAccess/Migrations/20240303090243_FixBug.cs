using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class FixBug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staff_EngineerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staff_ManagerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestForms_Materials_MaterialId",
                table: "RequestForms");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Accounts_UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingReports_Staff_StaffId",
                table: "WorkingReports");

            migrationBuilder.DropIndex(
                name: "IX_RequestForms_MaterialId",
                table: "RequestForms");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "RequestForms");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RejectionReports");

            migrationBuilder.DropColumn(
                name: "UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_ManagerId",
                table: "Staffs",
                newName: "IX_Staffs_ManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDay",
                table: "RejectionReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDay",
                table: "RejectionReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitDay",
                table: "RejectionReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CustomQuotations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Staffs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Username",
                table: "Customers",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_Username",
                table: "Staffs",
                column: "Username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_Username",
                table: "Customers",
                column: "Username",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectionReports_Staffs_EngineerId",
                table: "RejectionReports",
                column: "EngineerId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectionReports_Staffs_ManagerId",
                table: "RejectionReports",
                column: "ManagerId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Accounts_Username",
                table: "Staffs",
                column: "Username",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Staffs_ManagerId",
                table: "Staffs",
                column: "ManagerId",
                principalTable: "Staffs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingReports_Staffs_StaffId",
                table: "WorkingReports",
                column: "StaffId",
                principalTable: "Staffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_Username",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staffs_EngineerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staffs_ManagerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Accounts_Username",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Staffs_ManagerId",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingReports_Staffs_StaffId",
                table: "WorkingReports");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Username",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_Username",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ReceiveDay",
                table: "RejectionReports");

            migrationBuilder.DropColumn(
                name: "RejectedDay",
                table: "RejectionReports");

            migrationBuilder.DropColumn(
                name: "SubmitDay",
                table: "RejectionReports");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_ManagerId",
                table: "Staff",
                newName: "IX_Staff_ManagerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tasks",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "MaterialId",
                table: "RequestForms",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RejectionReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "CustomQuotations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsernameNavigationUsername",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Staff",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "UsernameNavigationUsername",
                table: "Staff",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForms_MaterialId",
                table: "RequestForms",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers",
                column: "UsernameNavigationUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UsernameNavigationUsername",
                table: "Staff",
                column: "UsernameNavigationUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_UsernameNavigationUsername",
                table: "Customers",
                column: "UsernameNavigationUsername",
                principalTable: "Accounts",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_RejectionReports_Staff_EngineerId",
                table: "RejectionReports",
                column: "EngineerId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RejectionReports_Staff_ManagerId",
                table: "RejectionReports",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForms_Materials_MaterialId",
                table: "RequestForms",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Accounts_UsernameNavigationUsername",
                table: "Staff",
                column: "UsernameNavigationUsername",
                principalTable: "Accounts",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                table: "Staff",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingReports_Staff_StaffId",
                table: "WorkingReports",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
