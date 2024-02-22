using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class ConstructDetail
    {
        public string QuotationId { get; set; } = null!;
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public int Facade { get; set; }
        public string Alley { get; set; } = null!;
        public int Floor { get; set; }
        public int Room { get; set; }
        public decimal Mezzanine { get; set; }
        public decimal RooftopFloor { get; set; }
        public bool Balcony { get; set; }
        public decimal Garden { get; set; }
        public string ConstructionId { get; set; } = null!;
        public string InvestmentId { get; set; } = null!;
        public string FoundationId { get; set; } = null!;
        public string RooftopId { get; set; } = null!;
        public string BasementId { get; set; } = null!;

        public virtual BasementType Basement { get; set; } = null!;
        public virtual ConstructionType Construction { get; set; } = null!;
        public virtual FoundationType Foundation { get; set; } = null!;
        public virtual InvestmentType Investment { get; set; } = null!;
        public virtual CustomQuotation? Quotation { get; set; } = null!;
        public virtual RooftopType Rooftop { get; set; } = null!;
    }
}
