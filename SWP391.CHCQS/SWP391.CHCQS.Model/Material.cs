using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Material
    {
        public Material()
        {
            //MaterialDetails = new HashSet<MaterialDetail>();
            Combos = new HashSet<Combo>();            
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        [Required(ErrorMessage ="Vui lòng nhập Tên Vật Liệu")]
		[Display(Name = "Tên Vật Liệu")]
		public string Name { get; set; } = null!;
        [Display(Name = "Giá Gốc")]
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
        public string CategoryId { get; set; } = null!;
        [ForeignKey("CategoryId")]
        public virtual MaterialCategory Category { get; set; } = null!;
        //public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
        [MaxLength(200)]

        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        public virtual ICollection<Combo> Combos { get; set; }        

    }
}
