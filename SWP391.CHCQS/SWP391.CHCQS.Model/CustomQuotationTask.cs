using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class CustomQuotationTask
    {
        public string TaskId { get; set; } = null!;
        public string QuotationId { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual CustomQuotation Quotation { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
    }
}
