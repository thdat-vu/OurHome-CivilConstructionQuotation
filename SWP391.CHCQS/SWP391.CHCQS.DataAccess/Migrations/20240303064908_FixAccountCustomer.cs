using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class FixAccountCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UsernameNavigationUsername",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UsernameNavigationUsername",
                table: "Customers");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Accounts_Username",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_Username",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "UsernameNavigationUsername",
                table: "Customers",
                type: "nvarchar(100)",
                nullable: true);

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
        }
    }
}
