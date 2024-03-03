using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class FixAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Accounts_UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UsernameNavigationUsername",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Staff",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Username",
                table: "Staff",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Username",
                table: "Customers",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_Username",
                table: "Customers",
                column: "Username",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Accounts_Username",
                table: "Staff",
                column: "Username",
                principalTable: "Accounts",
                principalColumn: "Username",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_Username",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Accounts_Username",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_Username",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Username",
                table: "Customers");

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

            migrationBuilder.AddColumn<string>(
                name: "UsernameNavigationUsername",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_UsernameNavigationUsername",
                table: "Staff",
                column: "UsernameNavigationUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers",
                column: "UsernameNavigationUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Accounts_UsernameNavigationUsername",
                table: "Customers",
                column: "UsernameNavigationUsername",
                principalTable: "Accounts",
                principalColumn: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Accounts_UsernameNavigationUsername",
                table: "Staff",
                column: "UsernameNavigationUsername",
                principalTable: "Accounts",
                principalColumn: "Username");
        }
    }
}
