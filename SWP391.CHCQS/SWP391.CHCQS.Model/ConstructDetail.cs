using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class ConstructDetail
    {

        [Key]
        [Display(Name = "Mã công trình")]
        [MaxLength(10, ErrorMessage = "Chiều dài {0} không được vượt quá {1} ký tự")]
        public string QuotationId { get; set; } = null!;

        [Display(Name = "Chiều rộng")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Range(0.00000001,1000, ErrorMessage = "{0} phải lớn hơn 0")]
		[RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Giá trị nhập phải là chữ số")]
		public decimal Width { get; set; }


        [Display(Name = "Chiều dài")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
		[Range(0.00000001, 1000, ErrorMessage = "{0} phải lớn hơn 0")]
		[RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Giá trị nhập phải là chữ số")]
		public decimal Length { get; set; }


        [Display(Name = "Mặt tiền")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public int Facade { get; set; }


        [MaxLength(50, ErrorMessage = "{0} không được quá {1} ký tự")]
		[Display(Name = "loại hẻm")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string Alley { get; set; } = null!;


		[Display(Name = "Số tầng")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		[Range(1, 100, ErrorMessage = "{0} trong khoảng từ {1} đến {2}")]
        public int Floor { get; set; }


		[Display(Name = "Số phòng")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		[Range(1, 100, ErrorMessage = "{0} trong khoảng từ {1} đến {2}")]
		[RegularExpression(@"^-?\d+$", ErrorMessage = "Giá trị nhập phải là chữ số")]
		public int Room { get; set; }


		[Display(Name = "Diện tích lửng")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		[Range(0.00000001, 1000, ErrorMessage = "{0} phải lớn hơn 0")]
		[RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Giá trị nhập phải là chữ số")]
		public decimal Mezzanine { get; set; }


		[Display(Name = "Diện tích tầng thượng")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		[Range(0.00000001, 1000, ErrorMessage = "{0} phải lớn hơn 0")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Giá trị nhập phải là chữ số")]
		public decimal RooftopFloor { get; set; }

		[Display(Name = "loại ban công")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public bool Balcony { get; set; }
		[Display(Name = "Diện tích vườn")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
        [Range(0.00000001, 1000, ErrorMessage = "{0} phải lớn hơn 0")]
        [RegularExpression(@"^-?\d+(\.\d+)?$", ErrorMessage = "Giá trị nhập phải là chữ số")]
        public decimal Garden { get; set; }
        [MaxLength(10)]
		[Display(Name = "loại công trình")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string ConstructionId { get; set; } = null!;
        [MaxLength(10)]
		[Display(Name = "loại đầu tư")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string InvestmentId { get; set; } = null!;
        [MaxLength(10)]
		[Display(Name = "loại móng")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string FoundationId { get; set; } = null!;
        [MaxLength(10)]
		[Display(Name = "loại mái")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string RooftopId { get; set; } = null!;
        [MaxLength(10)]
		[Display(Name = "loại hầm")]
		[Required(ErrorMessage = "Cần chọn {0}")]
		public string BasementId { get; set; } = null!;
 
        public virtual BasementType? Basement { get; set; } 
        public virtual ConstructionType? Construction { get; set; }
        public virtual FoundationType? Foundation { get; set; } 
        public virtual InvestmentType? Investment { get; set; } 
        public virtual CustomQuotation? Quotation { get; set; } 
        public virtual RooftopType? Rooftop { get; set; } 

    }
}
