	using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Project
    {
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(200)]
		[Required(ErrorMessage = "Vui lòng nhập Tên Dự Án")]
		[Display(Name = "Tên Dự Án")]
		public string Name { get; set; } = null!;
        [MaxLength(200)]
		[Required(ErrorMessage = "Vui lòng nhập Vị trí")]
		[Display(Name = "Vị trí")]
		public string Location { get; set; } = null!;
        [MaxLength(200)]
		[Required(ErrorMessage = "Vui lòng nhập Quy mô")]
		[Display(Name = "Quy mô")]
		public string Scale { get; set; } = null!;
        [MaxLength(100)]
		[Required(ErrorMessage = "Vui lòng nhập Diện tích")]
		[Display(Name = "Diện tích")]
		public string Size { get; set; } = null!;
        [MaxLength(500)]
        [Required(ErrorMessage = "Vui lòng nhập Mô tả")]
        [Display(Name = "Mô tả")]
		public string? Description { get; set; }
		[Display(Name = "Tổng quan")]
        [Required(ErrorMessage = "Vui lòng nhập Tổng quan")]
        public string? Overview { get; set; }        
        public bool Status { get; set; }
		[Required(ErrorMessage = "Vui lòng chọn Ngày phù hợp")]
		[Display(Name = "Ngày")]
		public DateTime Date { get; set; }
        public string CustomerId { get; set; } = null!;
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; } = null!;

        public virtual ICollection<ProjectImage> Images { get; set; }
    }
}
