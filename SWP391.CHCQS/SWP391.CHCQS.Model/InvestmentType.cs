using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class InvestmentType
    {
        public InvestmentType()
        {
            //ConstructDetails = new HashSet<ConstructDetail>();
            Pricings = new HashSet<Pricing>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        //public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
        public virtual ICollection<Pricing> Pricings { get; set; }
    }
}
