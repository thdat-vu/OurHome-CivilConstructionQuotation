using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class StandardQuotation
    {
        public StandardQuotation()
        {
            Materials = new HashSet<Material>();
            Tasks = new HashSet<Task>();
        }

        public string Id { get; set; } = null!;
<<<<<<< HEAD:SWP391.CHCQS/SWP391.CHCQS.Model/StandardQuotation.cs
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string ConstructionId { get; set; } = null!;

=======
        [MaxLength(200)]
		[Required(ErrorMessage = "Vui lòng nhập Tên Gói dịch vụ")]
		[Display(Name = "Tên Gói dịch vụ")]
		public string Name { get; set; } = null!;
        [MaxLength(500)]
		[Required(ErrorMessage = "Vui lòng nhập Mô tả")]
		[Display(Name = "Mô tả")]
		public string Description { get; set; }
		[Display(Name = "Giá tham khảo")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Vui lòng nhập một số hợp lệ.")]
        [Range(1, double.MaxValue, ErrorMessage = "Vui lòng nhập Giá lớn hơn 0.")]
		[Required(ErrorMessage = "Giá không được để trống ")]
		public decimal Price { get; set; }
        public bool Status { get; set; }
        [MaxLength(10)]
		[Required(ErrorMessage = "Vui lòng chọn Loại Công trình")]
		[Display(Name = "Loại công trình")]
		public string ConstructionId { get; set; } = null!;
        [ForeignKey("ConstructionId")]
>>>>>>> Demostration:SWP391.CHCQS/SWP391.CHCQS.Model/Combo.cs
        public virtual ConstructionType Construction { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
<<<<<<< HEAD:SWP391.CHCQS/SWP391.CHCQS.Model/StandardQuotation.cs
=======
        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }
>>>>>>> Demostration:SWP391.CHCQS/SWP391.CHCQS.Model/Combo.cs
    }
}
