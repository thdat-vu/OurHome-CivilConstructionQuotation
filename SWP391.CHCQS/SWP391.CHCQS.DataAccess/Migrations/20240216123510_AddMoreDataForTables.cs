using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddMoreDataForTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "username", "password", "role" },
                values: new object[,]
                {
                    { "anhnth", "1", "admin" },
                    { "bthuong", "1", "customer" },
                    { "hoanguyen", "1", "customer" },
                    { "lanly22", "1", "customer" },
                    { "phai789", "1", "customer" },
                    { "vnam", "1", "customer" }
                });

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1871));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID001",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { "thaonguyen123@gmail.com", "female", "0512369874" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID002",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { "mai.tran@email.com", "female", "0987654321" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID003",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { "minh.le@example.com", "male", "0123456789" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID004",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { "ngocanh.nguyen@email.com", "female", "0765432198" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID005",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { "tuan.minh@example.com", "male", "0345678901" });

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1837));

            migrationBuilder.InsertData(
                table: "RequestForm",
                columns: new[] { "id", "acreage", "constructType", "customerId", "description", "generateDate", "location", "status" },
                values: new object[,]
                {
                    { "RF002", "340m2", "CT1", "ID002", "Customer said that this project must be finished on 6 month", new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1850), "Quận 5, TP. Hồ Chí Minh", true },
                    { "RF003", "340m2", "CT3", "ID003", "Customer said that this project must be finished on 12 month", new DateTime(2024, 2, 16, 19, 35, 9, 544, DateTimeKind.Local).AddTicks(1853), "Long Thạnh Mỹ, TP. Thủ Đức", true }
                });

            migrationBuilder.InsertData(
                table: "RooftopType",
                columns: new[] { "id", "description", "name", "unitPrice" },
                values: new object[,]
                {
                    { "RT1", "Mái tôn", "Mái tôn", 3300000.00m },
                    { "RT2", "Mái BTCT", "Mái BTCT", 330000.00m },
                    { "RT3", "Mái ngói + Xà gồ", "Mái ngói + Xà gồ", 3300000.00m },
                    { "RT4", "Mái ngói + BTCT", "Mái ngói + BTCT", 3300000.00m }
                });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "EN001",
                column: "gender",
                value: "male");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "MG001",
                column: "gender",
                value: "male");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "SL001",
                column: "gender",
                value: "male");

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "id", "email", "gender", "name", "phoneNum", "username" },
                values: new object[,]
                {
                    { "ID006", "huong.bui@email.com", "female", "Bùi Thị Hương", "0876543210", "bthuong" },
                    { "ID007", "hai.pham@email.com", "male", "Phạm Văn Hải", "0567890123", "phai789" },
                    { "ID008", "lan.ly@example.com", "female", "Lý Thị Lan", "0234567890", "lanly22" },
                    { "ID009", "nam.vu@email.com", "male", "Vũ Thanh Nam", "0987654321", "vnam" },
                    { "ID010", "hoa.nguyen@email.com", "female", "Nguyễn Thị Hoa", "0456789012", "hoanguyen" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "id", "email", "gender", "managerId", "name", "phoneNum", "status", "username" },
                values: new object[] { "ADMIN", "anhnth@gmail.com", "female", null, "Nguyen Thach Ha Anh", "0987654321", true, "anhnth" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID006");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID007");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID008");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID009");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID010");

            migrationBuilder.DeleteData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF002");

            migrationBuilder.DeleteData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF003");

            migrationBuilder.DeleteData(
                table: "RooftopType",
                keyColumn: "id",
                keyValue: "RT1");

            migrationBuilder.DeleteData(
                table: "RooftopType",
                keyColumn: "id",
                keyValue: "RT2");

            migrationBuilder.DeleteData(
                table: "RooftopType",
                keyColumn: "id",
                keyValue: "RT3");

            migrationBuilder.DeleteData(
                table: "RooftopType",
                keyColumn: "id",
                keyValue: "RT4");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "ADMIN");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "anhnth");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "bthuong");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "hoanguyen");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "lanly22");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "phai789");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "vnam");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 16, 17, 12, 56, 534, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID001",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID002",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID003",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID004",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID005",
                columns: new[] { "email", "gender", "phoneNum" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 17, 12, 56, 534, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "EN001",
                column: "gender",
                value: "Men");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "MG001",
                column: "gender",
                value: "Men");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "id",
                keyValue: "SL001",
                column: "gender",
                value: "Men");
        }
    }
}
