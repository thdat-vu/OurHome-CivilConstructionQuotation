using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class DumplingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Customer",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 6);

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "username", "password", "role" },
                values: new object[,]
                {
                    { "dtuan", "1", "customer" },
                    { "lvm123", "1", "customer" },
                    { "maitran1", "1", "customer" },
                    { "ngocanh85", "1", "customer" },
                    { "thao123", "1", "customer" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "id", "email", "gender", "name", "phoneNum", "username" },
                values: new object[,]
                {
                    { "ID001", null, null, "Nguyễn Trần Phương Thảo", null, "thao123" },
                    { "ID002", null, null, "Trần Thị Mai", null, "maitran1" },
                    { "ID003", null, null, "Lê Văn Minh", null, "lvm123" },
                    { "ID004", null, null, "Ngọc Anh Nguyễn", null, "ngocanh85" },
                    { "ID005", null, null, "Đỗ Minh Tuấn", null, "dtuan" }
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "id", "customerId", "description", "ImageUrl", "location", "name", "Overview", "scale", "size", "status" },
                values: new object[,]
                {
                    { "PRJ01", "ID001", "Nhà ở gia đình", null, "Phường Hố Nai, thành phố Biên Hòa, tỉnh Đồng Nai", "NHÀ PHỐ CHỊ THẢO TẠI ĐỒNG NAI", null, "1 trệt, 2 lầu", "5x12", true },
                    { "PRJ02", "ID002", "Nhà ở gia đình", null, "huyện Bến Lức, tỉnh Long An", "NHÀ PHỐ CHỊ MAI", null, "1 trệt, 2 lầu, sân thượng", "5x21", true },
                    { "PRJ03", "ID003", "Nhà ở gia đình", null, "Phường An Phú Đông, Quận 12", "NHÀ PHỐ HIỆN ĐẠI 5 TẦNG CỦA ANH MINH", null, "1 trệt + 1 lửng + 2 lầu + 1 tum, sân thượng", "4.5x18", true },
                    { "PRJ04", "ID004", "Nhà ở gia đình", null, "Phường Hiệp Bình Chánh, TP. Thủ Đức", "NHÀ CHỊ NGỌC ANH", null, "1 trệt + 2 lầu + 1 tum, sân thượng", "4.35x19.5", true },
                    { "PRJ05", "ID005", "Nhà ở gia đình", null, "Quận 5, TP. HCM", "NHÀ 1 TRỆT 3 LẦU ANH TUẤN ", null, "Nhà 1 trệt 3 lầu có sân thượng", "4x17", true }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ01");

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ02");

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ03");

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ04");

            migrationBuilder.DeleteData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ05");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID001");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID002");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID003");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID004");

            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "id",
                keyValue: "ID005");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "dtuan");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "lvm123");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "maitran1");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "ngocanh85");

            migrationBuilder.DeleteData(
                table: "Account",
                keyColumn: "username",
                keyValue: "thao123");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Customer",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 6,
                oldNullable: true);
        }
    }
}
