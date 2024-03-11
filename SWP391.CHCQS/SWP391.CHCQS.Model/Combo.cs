using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Combo
    {
        public Combo()
        {
            Materials = new HashSet<Material>();
            Tasks = new HashSet<Task>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(200)]
		[Required(ErrorMessage = "Vui lòng nhập Tên Gói dịch vụ")]
		[Display(Name = "Tên Gói dịch vụ")]
		public string Name { get; set; } = null!;
        [MaxLength(500)]
		[Required(ErrorMessage = "Vui lòng nhập Mô tả")]
		[Display(Name = "Mô tả")]
		public string? Description { get; set; }
		[Display(Name = "Giá")]
		[Range(1, double.MaxValue, ErrorMessage = "Vui lòng nhập Giá lớn hơn 0.")]
		[Required(ErrorMessage = "Giá không được để trống ")]
		public decimal Price { get; set; }
        public bool Status { get; set; }
        [MaxLength(10)]
		[Required(ErrorMessage = "Vui lòng chọn Loại Công trình")]
		[Display(Name = "Loại công trình")]
		public string ConstructionId { get; set; } = null!;
        [ForeignKey("ConstructionId")]
        public virtual ConstructionType Construction { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public string? ImageUrl { get; set; }
    }
}
