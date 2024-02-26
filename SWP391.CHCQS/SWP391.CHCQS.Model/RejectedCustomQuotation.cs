using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    //class chứa các custom quotation bị từ chối bởi quản lý
    public class RejectedCustomQuotation
    {
        [Key]
        public string Id { get; set; }

        // Ngày bị reject
        public DateTime Date { get; set; }

        //---------------------------------------------------------------------------------------------
        // Xác định chi tiết công việc, vật liệu bị hủy của báo giá nào 
        [ForeignKey("RejectedQuotation")]
        public string? RejectedQuotationId { get; set; } = null!;
        public virtual CustomQuotation? RejectedQuotation { get; set; } = null!;

        //---------------------------------------------------------------------------------------------
        // Engineer đã thực hiện báo giá này
        [ForeignKey("Engineer")]
        public string? EngineerId { get; set; } = null!;
        public virtual Staff? Engineer { get; set; } =  null!;

        //---------------------------------------------------------------------------------------------
        // Manager đã reject báo giá này
        [ForeignKey("Manager")]
        public string? ManagerId { get; set; } = null!;
        public virtual Staff? Manager { get; set; } =  null!;

        //---------------------------------------------------------------------------------------------
        // Người thực hiện báo giá này
        [ForeignKey("Subcriber")]
        public string? SubcriberId { get; set; } = null!;
        public virtual Staff? Subcriber { get; set; } = null!;

        public string? Reason { get; set; }
    }
}
