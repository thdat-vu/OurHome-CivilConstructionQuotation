using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class DumplingStaffCustomQuotationRequestFormTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "username", "password", "role" },
                values: new object[,]
                {
                    { "datnt", "1", "engineer" },
                    { "datnx", "1", "manager" },
                    { "duclm", "1", "seller" }
                });

            migrationBuilder.InsertData(
                table: "RequestForm",
                columns: new[] { "id", "acreage", "constructType", "customerId", "description", "generateDate", "location", "status" },
                values: new object[] { "RF001", "240m2", "CT2", "ID001", "Customer said that this project must be finished on 3 month", new DateTime(2024, 2, 15, 23, 46, 51, 457, DateTimeKind.Local).AddTicks(7517), "Dĩ An, Bình Dương", true });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "id", "email", "gender", "managerId", "name", "phoneNum", "status", "username" },
                values: new object[] { "MG001", "datnx@gmail.com", "Men", null, "Nguyen Xuan Dat", "0987654321", true, "datnx" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "id", "email", "gender", "managerId", "name", "phoneNum", "status", "username" },
                values: new object[] { "EN001", "datnt@gmail.com", "Men", "MG001", "Nguyen Thanh Dat", "0987654321", true, "datnt" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "id", "email", "gender", "managerId", "name", "phoneNum", "status", "username" },
                values: new object[] { "SL001", "duclm@gmail.com", "Men", "MG001", "Le Minh Duc", "0987654321", true, "duclm" });

            migrationBuilder.InsertData(
                table: "CustomQuotation",
                columns: new[] { "id", "acreage", "Date", "description", "engineerId", "location", "managerId", "requestId", "sellerId", "status", "total" },
                values: new object[] { "CQ001", "240m2", new DateTime(2024, 2, 15, 23, 46, 51, 457, DateTimeKind.Local).AddTicks(7553), "I want to build this house for my son and his wife, so i can live with them", "EN001", "Dĩ An, Bình Dương", "MG001", "RF001", "SL001", true, 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001");

            migrationBuilder.DeleteData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "EN001");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "SL001");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "datnt");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "duclm");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "MG001");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "datnx");
        }
    }
}
