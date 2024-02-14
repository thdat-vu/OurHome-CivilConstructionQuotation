using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Pricing
    {
        public string ConstructTypeId { get; set; } = null!;
        public string InvestmentTypeId { get; set; } = null!;
        public decimal? UnitPrice { get; set; }

        public virtual ConstructionType ConstructType { get; set; } = null!;
        public virtual InvestmentType InvestmentType { get; set; } = null!;
    }
}
