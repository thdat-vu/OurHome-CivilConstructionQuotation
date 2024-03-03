using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class UpdateStaffTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staff_EngineerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_RejectionReports_Staff_ManagerId",
                table: "RejectionReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Accounts_Username",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Staff_ManagerId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingReports_Staff_StaffId",
                table: "WorkingReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_Username",
                table: "Staffs",
                newName: "IX_Staffs_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_ManagerId",
                table: "Staffs",
                newName: "IX_Staffs_ManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_Username",
                table: "Staff",
                newName: "IX_Staff_Username");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_ManagerId",
                table: "Staff",
                newName: "IX_Staff_ManagerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

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
                name: "FK_Staff_Accounts_Username",
                table: "Staff",
                column: "Username",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

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
