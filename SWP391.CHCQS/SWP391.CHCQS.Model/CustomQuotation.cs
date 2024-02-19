using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class CustomQuotation
    {
        public CustomQuotation()
        {
            CustomQuotaionTasks = new HashSet<CustomQuotaionTask>();
            MaterialDetails = new HashSet<MaterialDetail>();
        }
        [Key]
        public string Id { get; set; } = null!;
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
        public int Status { get; set; }
        public string? Description { get; set; }
        //số tiền mong muốn sẽ sử dụng cho việc thi công
        public decimal Total { get; set; }
        public virtual ConstructDetail? ConstructDetail { get; set; }
//---------------------------------------------------------------------------------------------
        [ForeignKey("RequestForm")]
        public string RequestId { get; set; } = null!;
        public virtual RequestForm Request { get; set; } = null!;
//---------------------------------------------------------------------------------------------
        //Chi tiết ủy quyền công việc: cho ai, thời gian nhận và thời gian hoàn thành
        [ForeignKey("CustomQuotationDelegation")]
        public string DelegationId { get; set; } = null!;
        public virtual CustomQuotationDelegation Delegation { get; set; } = null!;
//---------------------------------------------------------------------------------------------
        //công việc Engineer phải xử lý
        public virtual ICollection<CustomQuotaionTask> CustomQuotaionTasks { get; set; }
        public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
    }
}
