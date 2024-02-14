using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class RooftopType
    {
        public RooftopType()
        {
            ConstructDetails = new HashSet<ConstructDetail>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
    }
}
