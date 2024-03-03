using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class TaskDetail
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string TaskId { get; set; } = null!;
        [MaxLength(10)]
        public string QuotationId { get; set; } = null!;
        
        public decimal Price { get; set; }

        [ForeignKey("QuotationId")]
        public virtual CustomQuotation Quotation { get; set; } = null!;
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; } = null!;
    }
}
