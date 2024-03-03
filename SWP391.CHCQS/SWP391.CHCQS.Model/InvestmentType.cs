using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class InvestmentType
    {
        public InvestmentType()
        {
            //ConstructDetails = new HashSet<ConstructDetail>();
            Pricings = new HashSet<Pricing>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }

        //public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
        public virtual ICollection<Pricing> Pricings { get; set; }
    }
}
