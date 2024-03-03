using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public class RejectionReport
    {
        [Key]
        [MaxLength(10)]
        public string Id { get; set; }

        public DateTime? SubmitDay { get; set; } //ngày engineer submit báo giá
        public DateTime? ReceiveDay { get; set; } //ngày manager xem báo giá

        public DateTime? RejectedDay { get; set; } //ngày manager reject báo giá
        

        //---------------------------------------------------------------------------------------------
        // Xác định chi tiết công việc, vật liệu bị hủy của báo giá nào 
        [ForeignKey("RejectedQuotation")]
        [MaxLength(10)]
        public string RejectedQuotationId { get; set; }
        
        public virtual CustomQuotation RejectedQuotation { get; set; }

        //---------------------------------------------------------------------------------------------
        // Engineer đã thực hiện báo giá này
        [ForeignKey("Engineer")]
        public string EngineerId { get; set; }
        public virtual Staff Engineer { get; set; }

        //---------------------------------------------------------------------------------------------
        // Manager đã reject báo giá này
        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        public virtual Staff Manager { get; set; }

        //---------------------------------------------------------------------------------------------
        [MaxLength(500)]
        public string? Reason { get; set; }
    }

}
