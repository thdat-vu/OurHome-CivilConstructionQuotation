using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddFullDataForAllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvestmentTypeID",
                table: "Pricing",
                newName: "InvestmentTypeId");

            migrationBuilder.RenameColumn(
                name: "ConstructTypeID",
                table: "Pricing",
                newName: "ConstructTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pricing_InvestmentTypeID",
                table: "Pricing",
                newName: "IX_Pricing_InvestmentTypeId");

            migrationBuilder.InsertData(
                table: "BasementType",
                columns: new[] { "id", "description", "name", "unitPrice" },
                values: new object[,]
                {
                    { "BT1", "Không có hầm", "Không Hầm", 0m },
                    { "BT2", "Hầm Độ Sâu 1.0 - 1.3 m", "Độ Sâu 1.0 - 1.3", 3400000m },
                    { "BT3", "Hầm Độ Sâu 1.3 - 1.7 m", "Độ Sâu 1.3 - 1.7", 4400000m },
                    { "BT4", "Hầm Độ Sâu 1.7 - 2.0 m", "Độ sâu 1.7 - 2.0", 5400000m },
                    { "BT5", "Hầm Độ sâu Lớn Hơn 2.0 m", "Độ Sâu >2.0", 6400000m }
                });

            migrationBuilder.InsertData(
                table: "ConstructionType",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { "CT1", "Nhà ở thành phố đông đúc, diện tích đất hẹp.", "Nhà Phố" },
                    { "CT2", "Quy mô lớn, kiến trúc đẹp, đất rộng.", "Biệt thự" },
                    { "CT3", "Nhà cơ bản, chi phí rẻ, thông dụng, đất dài.", "Nhà cấp bốn " }
                });

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 16, 17, 12, 56, 534, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.InsertData(
                table: "FoundationType",
                columns: new[] { "id", "areaRatio", "description", "name", "unitPrice" },
                values: new object[,]
                {
                    { "FT1", 0.30m, "Móng đơn", "Móng Đơn", 3200000m },
                    { "FT2", 0.65m, "Móng bằng", "Móng Bằng", 4200000m },
                    { "FT3", 0.50m, "Móng đài cọc", "Móng Đài Cọc", 5200000m }
                });

            migrationBuilder.InsertData(
                table: "InvestmentType",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { "IT1", "Xây nhà phần thô", "Xây nhà phần thô" },
                    { "IT2", "Xây nhà trọn gói", "Xây nhà trọn gói" },
                    { "IT3", "Mức trung bình", "Mức TB" },
                    { "IT4", "Mức khá", "Mức Khá" },
                    { "IT5", "Mức khá +", "Mức Khá +" }
                });

            migrationBuilder.InsertData(
                table: "MaterialCategory",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "VT1", "Vật tư thô" },
                    { "VT2", "Sơn nước sơn dầu" },
                    { "VT3", "Điện" },
                    { "VT4", "Vệ sinh" },
                    { "VT5", "Bếp" },
                    { "VT6", "Cầu thang" },
                    { "VT7", "Cửa" },
                    { "VT8", "Gạch ốp lát" },
                    { "VT9", "Trần" }
                });

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 16, 17, 12, 56, 534, DateTimeKind.Local).AddTicks(6493));

            migrationBuilder.InsertData(
                table: "TaskCategory",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "TKB", "Đầu mục cơ bản" },
                    { "TKC", "Đầu mục hoàn thiện" }
                });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "id", "categoryId", "inventoryQuantity", "name", "status", "unit", "unitPrice" },
                values: new object[,]
                {
                    { "VT101", "VT1", 5000, "Sắt thép Việt Nhật", true, "m", 0.0000m },
                    { "VT102", "VT1", 5000, "Xi măng đổ bê tông Holcim", true, "bao", 0.0000m },
                    { "VT103", "VT1", 5000, "Xi măng xây tô tường Hà Tiên", true, "bao", 0.0000m },
                    { "VT104", "VT1", 5000, "Bê tông tươi Lê Phan - Hoàng Sở M250", true, "m3", 0.0000m },
                    { "VT105", "VT1", 5000, "Cát hạt lớn", true, "m3", 0.0000m },
                    { "VT106", "VT1", 5000, "Cát hạt vàng trung", true, "m3", 0.0000m },
                    { "VT107", "VT1", 5000, "Đá xanh Đồng Nai", true, "ton", 0.0000m },
                    { "VT108", "VT1", 5000, "Gạch đinh 8x8x18 Tuynel Bình Dương", true, "viên", 0.0000m },
                    { "VT109", "VT1", 5000, "Gạch định 4x8x18 Tuynel Bình Dương", true, "viên", 0.0000m },
                    { "VT110", "VT1", 5000, "Cáp TV Sino", true, "m", 0.0000m },
                    { "VT111", "VT1", 5000, "Cáp TV Sino (Panasonic)", true, "m", 0.0000m },
                    { "VT112", "VT1", 5000, "Cáp mạng Sino", true, "m", 0.0000m },
                    { "VT113", "VT1", 5000, "Cáp mạng Sino (Panasonic)", true, "m", 0.0000m },
                    { "VT114", "VT1", 5000, "Đế âm tường Sino", true, "cái", 0.0000m },
                    { "VT115", "VT1", 5000, "Đường ống nóng âm tường Vesbo", true, "m", 0.0000m },
                    { "VT116", "VT1", 5000, "Đường ống cấp nước, thoát nước Bình Minh", true, "m", 0.0000m },
                    { "VT117", "VT1", 5000, "Hóa chất chống thấm ban công, sân thượng, WC Kova CF-11A, Sika - 1F", true, "thùng", 0.0000m }
                });

            migrationBuilder.InsertData(
                table: "Pricing",
                columns: new[] { "ConstructTypeId", "InvestmentTypeId", "UnitPrice" },
                values: new object[,]
                {
                    { "CT1", "IT1", 3400000.00m },
                    { "CT1", "IT2", 6000000.00m },
                    { "CT1", "IT3", 4800000.00m },
                    { "CT1", "IT4", 5400000.00m },
                    { "CT1", "IT5", 6000000.00m },
                    { "CT2", "IT1", 3600000.00m },
                    { "CT2", "IT2", 6400000.00m },
                    { "CT2", "IT3", 5000000.00m },
                    { "CT2", "IT4", 5700000.00m },
                    { "CT2", "IT5", 6400000.00m },
                    { "CT3", "IT1", 2400000.00m },
                    { "CT3", "IT2", 4700000.00m },
                    { "CT3", "IT3", 4700000.00m },
                    { "CT3", "IT4", 4700000.00m },
                    { "CT3", "IT5", 4700000.00m }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "id", "categoryId", "description", "name", "status", "unitPrice" },
                values: new object[,]
                {
                    { "TKS11", "TKB", "Lắp đặt hệ thống đường dây truyền hình cáp, internet", "Lắp đặt đường dây cáp", true, 4800000.00m },
                    { "TSK01", "TKB", "Tổ chức công trường, làm lán trại cho công nhân", "Tổ chức công trường", true, 4800000.00m },
                    { "TSK02", "TKB", "Vệ sinh mặt bằng thi công, định vị móng, cột", "Vệ sinh mặt bằng", true, 4800000.00m },
                    { "TSK03", "TKB", "Đào đất hố móng", "Đào móng", true, 4800000.00m },
                    { "TSK04", "TKB", "Thi công theo bản vẽ thiết kế", "Thi công phần trên", true, 4800000.00m },
                    { "TSK05", "TKB", "Thi công coffa, cốt thép, đổ bê tông móng, đà kiềng, dầm sàn các lầu, cột,... theo bản thiết kế", "Thi công coffa, cốt thép, đổ bê tông móng", true, 4800000.00m },
                    { "TSK06", "TKB", "Xây tường gạch 100mm, 8x8x18 theo bản thiết kế", "Xây tường gạch", true, 4800000.00m },
                    { "TSK07", "TKB", "Cán nền các nền lầu, sân thượng, mái và nhà vệ sinh", "Cán nền", true, 4800000.00m },
                    { "TSK08", "TKB", "Thi công chống thấm Sê nô, sàn mái, sàn vệ sinh, sân thượng,...", "Thi công chống thấm", true, 4800000.00m },
                    { "TSK09", "TKB", "Lắp đặt hệ thống đường ống cấp và thoát nước nóng lạnh", "Lắp đặt ống nước", true, 4800000.00m }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "id", "categoryId", "description", "name", "status", "unitPrice" },
                values: new object[,]
                {
                    { "TSK10", "TKB", "Lắp đặt hệ thống đường dây diện chiếu sáng, đế âm, hộp nối", "Lắp đặt đường dây điện", true, 4800000.00m },
                    { "TSK12", "TKB", "Vệ sinh công trình", "Vệ sinh công trình", true, 4800000.00m },
                    { "TSK13", "TKC", "Ốp lát gạch toàn bộ sàn của nhà, phòng bếp, tường bếp vệ sinh theo bản thiết kế", "Ốp gạch sàn nhà, bếp, tường", true, 4800000.00m },
                    { "TSK14", "TKC", "Ốp gạch, đá trang trí", "Ốp gạch trang trí", true, 4800000.00m },
                    { "TSK15", "TKC", "Lắp đặt hệ thống điện và chiếu sáng: công tắc, ổ cắm, bóng đèn ", "Lắp đặt hệ thống điện và chiếu sáng", true, 4800000.00m },
                    { "TSK16", "TKC", "Lắp đặt thiết bị vệ sinh: bàn cầu, lavabo, vòi nước,...", "Lắp đặt thiết bị vệ sinh", true, 4800000.00m },
                    { "TSK17", "TKC", "Dựng bao cửa gỗ, cửa sắt", "Dựng cửa", true, 4800000.00m },
                    { "TSK18", "TKC", "Trét mát tít và sơn nước toàn bộ bên trong và bên ngoài nhà", "Trét mát tít và sơn nước", true, 4800000.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BasementType",
                keyColumn: "id",
                keyValue: "BT1");

            migrationBuilder.DeleteData(
                table: "BasementType",
                keyColumn: "id",
                keyValue: "BT2");

            migrationBuilder.DeleteData(
                table: "BasementType",
                keyColumn: "id",
                keyValue: "BT3");

            migrationBuilder.DeleteData(
                table: "BasementType",
                keyColumn: "id",
                keyValue: "BT4");

            migrationBuilder.DeleteData(
                table: "BasementType",
                keyColumn: "id",
                keyValue: "BT5");

            migrationBuilder.DeleteData(
                table: "FoundationType",
                keyColumn: "id",
                keyValue: "FT1");

            migrationBuilder.DeleteData(
                table: "FoundationType",
                keyColumn: "id",
                keyValue: "FT2");

            migrationBuilder.DeleteData(
                table: "FoundationType",
                keyColumn: "id",
                keyValue: "FT3");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT101");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT102");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT103");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT104");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT105");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT106");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT107");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT108");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT109");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT110");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT111");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT112");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT113");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT114");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT115");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT116");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "id",
                keyValue: "VT117");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT2");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT3");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT4");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT5");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT6");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT7");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT8");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT9");

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT1", "IT1" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT1", "IT2" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT1", "IT3" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT1", "IT4" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT1", "IT5" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT2", "IT1" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT2", "IT2" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT2", "IT3" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT2", "IT4" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT2", "IT5" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT3", "IT1" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT3", "IT2" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT3", "IT3" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT3", "IT4" });

            migrationBuilder.DeleteData(
                table: "Pricing",
                keyColumns: new[] { "ConstructTypeId", "InvestmentTypeId" },
                keyValues: new object[] { "CT3", "IT5" });

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TKS11");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK01");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK02");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK03");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK04");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK05");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK06");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK07");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK08");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK09");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK10");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK12");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK13");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK14");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK15");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK16");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK17");

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "id",
                keyValue: "TSK18");

            migrationBuilder.DeleteData(
                table: "ConstructionType",
                keyColumn: "id",
                keyValue: "CT1");

            migrationBuilder.DeleteData(
                table: "ConstructionType",
                keyColumn: "id",
                keyValue: "CT2");

            migrationBuilder.DeleteData(
                table: "ConstructionType",
                keyColumn: "id",
                keyValue: "CT3");

            migrationBuilder.DeleteData(
                table: "InvestmentType",
                keyColumn: "id",
                keyValue: "IT1");

            migrationBuilder.DeleteData(
                table: "InvestmentType",
                keyColumn: "id",
                keyValue: "IT2");

            migrationBuilder.DeleteData(
                table: "InvestmentType",
                keyColumn: "id",
                keyValue: "IT3");

            migrationBuilder.DeleteData(
                table: "InvestmentType",
                keyColumn: "id",
                keyValue: "IT4");

            migrationBuilder.DeleteData(
                table: "InvestmentType",
                keyColumn: "id",
                keyValue: "IT5");

            migrationBuilder.DeleteData(
                table: "MaterialCategory",
                keyColumn: "id",
                keyValue: "VT1");

            migrationBuilder.DeleteData(
                table: "TaskCategory",
                keyColumn: "id",
                keyValue: "TKB");

            migrationBuilder.DeleteData(
                table: "TaskCategory",
                keyColumn: "id",
                keyValue: "TKC");

            migrationBuilder.RenameColumn(
                name: "InvestmentTypeId",
                table: "Pricing",
                newName: "InvestmentTypeID");

            migrationBuilder.RenameColumn(
                name: "ConstructTypeId",
                table: "Pricing",
                newName: "ConstructTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Pricing_InvestmentTypeId",
                table: "Pricing",
                newName: "IX_Pricing_InvestmentTypeID");

            migrationBuilder.UpdateData(
                table: "CustomQuotation",
                keyColumn: "id",
                keyValue: "CQ001",
                column: "Date",
                value: new DateTime(2024, 2, 15, 23, 46, 51, 457, DateTimeKind.Local).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "RequestForm",
                keyColumn: "id",
                keyValue: "RF001",
                column: "generateDate",
                value: new DateTime(2024, 2, 15, 23, 46, 51, 457, DateTimeKind.Local).AddTicks(7517));
        }
    }
}
