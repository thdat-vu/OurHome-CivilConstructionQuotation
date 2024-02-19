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
        public string Id { get; set; } = null!;
        //---------------------------------------------------------------------------------------------
        //Xác định chi tiết công việc, vật liệu bị hủy của báo giá nào 
        [ForeignKey("CustomQuotation")]
        public string RejectedQuotationId { get; set; } = null!;
        public virtual CustomQuotation RejectedQuotation { get; set; } = null!;
        //---------------------------------------------------------------------------------------------
        //công việc Engineer đưa lên bị reject thì đưa vào đây
        public virtual ICollection<CustomQuotaionTask> CustomQuotaionTasks { get; set; }
        public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
        //--------------------------------------------------------------------------------------------
        //Engineer đã thực hiện báo giá này
        [ForeignKey("Staff")]
        public string EngineerId { get; set; } = null!;
        public virtual Staff Subcriber { get; set; } = null!;
        //Manager đã reject báo giá này
        [ForeignKey("Staff")]
        public string ManagerId { get; set; } = null!;
        public virtual Staff Rejecter { get; set; } = null!;
    }
}
