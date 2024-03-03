using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class MaterialDetail
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string QuotationId { get; set; } = null!;
        [MaxLength(10)]
        public string MaterialId { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

        public virtual Material Material { get; set; } = null!;
        public virtual CustomQuotation Quotation { get; set; } = null!;
    }
}
