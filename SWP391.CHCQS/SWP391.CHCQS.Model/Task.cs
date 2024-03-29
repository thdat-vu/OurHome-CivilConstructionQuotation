using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Task
    {
        public Task()
        {
            //CustomQuotaionTasks = new HashSet<CustomQuotationTask>();
            Quotations = new HashSet<StandardQuotation>();
        }

        public string Id { get; set; } = null!;
<<<<<<< HEAD
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
        public string CategoryId { get; set; } = null!;
=======
        [MaxLength(200)]
		[Display(Name = "Tên Đầu Việc")]
		[Required(ErrorMessage = "Vui lòng nhập tên đầu việc")]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
		[Display(Name = "Mô Tả")]
		[Required(ErrorMessage = "Vui lòng nhập mô tả")]
		public string? Description { get; set; }
		[Display(Name = "Giá Gốc")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Vui lòng nhập một số hợp lệ.")]
        [Range(1, double.MaxValue, ErrorMessage = "Vui lòng nhập Giá Gốc lớn hơn 0.")]
		[Required(ErrorMessage = "Vui lòng nhập giá gốc ")]
		public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
        [MaxLength(10)]
		[Required(ErrorMessage = "Vui lòng chọn Loại Đầu Việc")]
		[Display(Name = "Loại Đầu Việc")]
		public string CategoryId { get; set; } = null!;
>>>>>>> Demostration

        public virtual TaskCategory Category { get; set; } = null!;
        //public virtual ICollection<CustomQuotationTask> CustomQuotaionTasks { get; set; }

        public virtual ICollection<StandardQuotation> Quotations { get; set; }
    }
}
