using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Task
    {
        public Task()
        {
            //CustomQuotaionTasks = new HashSet<CustomQuotationTask>();
            Combos = new HashSet<Combo>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
        [MaxLength(10)]
        public string CategoryId { get; set; } = null!;

        public virtual TaskCategory Category { get; set; } = null!;
        //public virtual ICollection<CustomQuotationTask> CustomQuotaionTasks { get; set; }

        public virtual ICollection<Combo> Combos { get; set; }
    }
}
