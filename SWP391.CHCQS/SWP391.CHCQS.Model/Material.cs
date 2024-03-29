using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Material
    {
        public Material()
        {
            //MaterialDetails = new HashSet<MaterialDetail>();
            Quotations = new HashSet<StandardQuotation>();
            Requests = new HashSet<RequestForm>();
        }

        public string Id { get; set; } = null!;
<<<<<<< HEAD
        public string Name { get; set; } = null!;
        public int InventoryQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public bool Status { get; set; }
=======
        [MaxLength(100)]
        [Required(ErrorMessage ="Vui lòng nhập Tên Vật Liệu")]
		[Display(Name = "Tên Vật Liệu")]
		public string Name { get; set; } = null!;
        [Display(Name = "Giá Gốc")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Vui lòng nhập một số hợp lệ.")]
        [Range(1, double.MaxValue, ErrorMessage = "Vui lòng nhập Giá Gốc lớn hơn 0.")]
		[Required(ErrorMessage = "Giá Gốc không được để trống ")]
		public decimal UnitPrice { get; set; }
        [MaxLength(30)]
		[Display(Name = "Đơn vị")]
		[Required(ErrorMessage = "Vui lòng chọn Đơn Vị phù hợp")]
		public string Unit { get; set; } = null!;
        public bool Status { get; set; }
        [MaxLength(10)]
		[Required(ErrorMessage = "Vui lòng chọn Loại Vật Liệu")]
		[Display(Name = "Loại Vật Liệu")]
>>>>>>> Demostration
        public string CategoryId { get; set; } = null!;
        
        public virtual MaterialCategory Category { get; set; } = null!;
        //public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
<<<<<<< HEAD

        public virtual ICollection<StandardQuotation> Quotations { get; set; }
        public virtual ICollection<RequestForm> Requests { get; set; }
=======
        [MaxLength(200)]

        [Display(Name = "Hình ảnh")]
        public string? ImageUrl { get; set; }
        public virtual ICollection<Combo> Combos { get; set; }        
>>>>>>> Demostration

    }
}
