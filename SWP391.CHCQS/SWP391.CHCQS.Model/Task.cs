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
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
        public string CategoryId { get; set; } = null!;

        public virtual TaskCategory Category { get; set; } = null!;
        //public virtual ICollection<CustomQuotationTask> CustomQuotaionTasks { get; set; }

        public virtual ICollection<StandardQuotation> Quotations { get; set; }
    }
}
