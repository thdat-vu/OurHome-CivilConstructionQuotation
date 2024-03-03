using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Pricing
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [MaxLength(10)]
        public string ConstructTypeId { get; set; } = null!;
        [MaxLength(10)]
        public string InvestmentTypeId { get; set; } = null!;
        public decimal? UnitPrice { get; set; }

        public virtual ConstructionType ConstructType { get; set; } = null!;
        public virtual InvestmentType InvestmentType { get; set; } = null!;
    }
}
