using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class ProjectVM
	{
		public string Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Tên Dự Án")]
        [Display(Name = "Tên Dự Án")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Vị trí")]
        [Display(Name = "Vị trí")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Quy mô")]
        [Display(Name = "Quy mô")]
        public string Scale { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Diện tích")]
        [Display(Name = "Diện tích")]
        public string Size { get; set; }
		public bool Status { get; set; }
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Vui lòng nhập Mô tả")]
        public string? Description { get; set; }
        [Display(Name = "Tổng quan")]
        [Required(ErrorMessage = "Vui lòng nhập Tổng quan")]
        public string? Overview { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn Ngày phù hợp")]
        [Display(Name = "Ngày")]

        public DateTime Date { get; set; }
		[Required(ErrorMessage = "Vui lòng chọn ID Khách hàng")]
		[Display(Name = "ID Khách hàng")]
		//public Project Project { get; set; }
		public string CustomerId { get; set; }
		[Required(ErrorMessage = "Vui lòng chọn Tên Khách hàng")]
		[Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; }
        [Display(Name = "Hình ảnh")]
        public List<ProjectImage> Images { get; set; }
	}
}
