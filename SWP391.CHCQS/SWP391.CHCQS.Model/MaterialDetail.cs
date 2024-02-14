using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class MaterialDetail
    {
        public string QuotationId { get; set; } = null!;
        public string MaterialId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Material Material { get; set; } = null!;
        public virtual CustomQuotation Quotation { get; set; } = null!;
    }
}
