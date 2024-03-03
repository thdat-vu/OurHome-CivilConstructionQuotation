using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    [Serializable]
    public partial class CustomQuotation
    {
   
        public CustomQuotation()
        {
            TaskDetails = new HashSet<TaskDetail>();
            MaterialDetails = new HashSet<MaterialDetail>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        public DateTime Date { get; set; }
        [MaxLength(30)]
        public string? Acreage { get; set; }
        [MaxLength(100)]
        public string Location { get; set; } = null!;
        //các trạng thái
        //seller nhận xử lý: preparing (1)
        //TH bị hủy ở Seller: cancle (-1) - khách hàng ko nghe máy || ko thích tư vấn
        //engineer nhận xử lý: processing (2)
        //manager nhận:  Pending_Approval (3)
        //manager acceptance: Completed (4)
        //TH manager reject: rejected (0) -> quay lại processing khi engineer nhận (2)
        
		public int Status { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal Total { get; set; }
 


		public string RequestId { get; set; } = null!;
        [ForeignKey("RequestId")]
        public virtual RequestForm Request { get; set; } = null!;        
        public virtual ConstructDetail? ConstructDetail { get; set; } = null!;

        public ICollection<TaskDetail> TaskDetails { get; set; } = null!;
        public ICollection<MaterialDetail> MaterialDetails { get; set; } = null!;

        
    }
}
