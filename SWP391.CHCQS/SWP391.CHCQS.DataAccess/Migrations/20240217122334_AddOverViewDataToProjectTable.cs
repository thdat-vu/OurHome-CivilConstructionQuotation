using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391.CHCQS.DataAccess.Migrations
{
    public partial class AddOverViewDataToProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {            

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ01",
                column: "Overview",
                value: "Mô hình nhà phố hiện đại vẫn được ưa chuộng nhất qua các năm bởi vẻ đẹp đơn giản nhưng gian trọng và thanh lịch. Và chị Thảo là một trong rất nhiều khách hàng lựa chọn phong cách nhà này.\r\n\r\nVới dịch vụ hoàn thiện nhà trọn gói, chị đã sở hữu cho mình được căn nhà phố hiện đại 1 trệt 2 lầu đầy đủ tiện nghi, đảm bảo thẩm mỹ và độ bền. Cùng khám phá hình ảnh của ngôi nhà qua bài viết sau nhé!");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ02",
                column: "Overview",
                value: "Mô hình nhà phố hiện đại đang ngày càng trở nên phổ biến ở Việt Nam nhờ vẻ ngoài thẩm mỹ, sang trọng và chi phí xây dựng hợp lý.\r\n\r\nDưới sự hỗ trợ của dịch vụ thi công nhà trọn gói, gia đình Chị Mai tỉnh Long An thành công hoàn thiện ngôi nhà diện tích 105m2 với 1 trệt, 2 lầu theo phong cách hiện đại. Căn nhà không chỉ đáp ứng những công năng cần thiết chủ nhà yêu cầu mà còn đảm bảo yếu tố thẩm mỹ, phong thủy, chi phí tối ưu… ");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ03",
                column: "Overview",
                value: "Nhà ở không chỉ để ở, mà còn là nơi thể hiện phong cách, cá tính của gia chủ. Mỗi công trình nhà là một sản phẩm sáng tạo, mang dấu ấn riêng của người kiến trúc sư và chủ nhà.\r\n\r\nTọa lạc tại KDC Nam Long, Quận 12, ngôi nhà của anh Minh là một trong những công trình hiện đại nổi bật trong khu vực. Kiến trúc 5 tầng với kết cấu 1 trệt + 1 lửng + 2 tầng + 1 tum, căn nhà vừa đáp ứng được không gian sống thoải mái cho các thành viên, vừa tối ưu được chi phí thi công. Cùng Hưng Phú Thịnh ngắm nhìn hình ảnh thực tế của công trình này nhé.");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ04",
                column: "Overview",
                value: "Nằm giữa lòng thành phố Thủ Đức, ngôi nhà của chị Ngọc Anh là một điểm nhấn nổi bật với phong cách hiện đại, trẻ trung. Với diện tích gần 85m2, quy mô 1 trệt, 2 lầu và 1 tum sân thượng, căn nhà hoàn thiện không chỉ đáp ứng được công năng mà còn thỏa mãn được những yếu tố về thẩm mỹ, phong thủy, đem lại sự hài lòng cho gia chủ.");

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ05",
                column: "Overview",
                value: "Thiết kế nhà phố hiện đại 1 trệt 3 lầu sân thượng là lựa chọn hoàn hảo của nhiều gia đình ở thành phố lớn với diện tích xây dựng hạn chế. Mẫu nhà này có thể đáp ứng nhu cầu sinh hoạt tiện nghi, thoải mái nhưng vẫn đảm bảo riêng tư cho gia đình có đến 3 thế hệ cùng sinh sống.");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {          

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ01",
                column: "Overview",
                value: null);

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ02",
                column: "Overview",
                value: null);

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ03",
                column: "Overview",
                value: null);

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ04",
                column: "Overview",
                value: null);

            migrationBuilder.UpdateData(
                table: "Project",
                keyColumn: "id",
                keyValue: "PRJ05",
                column: "Overview",
                value: null);
            
        }
    }
}
