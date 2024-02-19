using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class CustomQuotation
    {
        public CustomQuotation()
        {
            CustomQuotaionTasks = new HashSet<CustomQuotaionTask>();
            MaterialDetails = new HashSet<MaterialDetail>();
        }

        public string Id { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
        public int Status { get; set; }
        public string? Description { get; set; }
        public decimal Total { get; set; }
        public string SellerId { get; set; } = null!;
        public string EngineerId { get; set; } = null!;
        public string ManagerId { get; set; } = null!;
        public string RequestId { get; set; } = null!;

        public virtual Staff Engineer { get; set; } = null!;
        public virtual Staff Manager { get; set; } = null!;
        public virtual RequestForm Request { get; set; } = null!;
        public virtual Staff Seller { get; set; } = null!;
        public virtual ConstructDetail? ConstructDetail { get; set; }
        public virtual ICollection<CustomQuotaionTask> CustomQuotaionTasks { get; set; }
        public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
    }
}
