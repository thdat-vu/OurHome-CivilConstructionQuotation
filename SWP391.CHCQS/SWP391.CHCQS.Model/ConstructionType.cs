using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class ConstructionType
    {
        public ConstructionType()
        {
            ConstructDetails = new HashSet<ConstructDetail>();
            Pricings = new HashSet<Pricing>();
            StandardQuotations = new HashSet<StandardQuotation>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
        public virtual ICollection<Pricing> Pricings { get; set; }
        public virtual ICollection<StandardQuotation> StandardQuotations { get; set; }
    }
}
