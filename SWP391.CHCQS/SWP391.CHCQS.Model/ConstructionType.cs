using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class ConstructionType
    {
        public ConstructionType()
        {
            //ConstructDetails = new HashSet<ConstructDetail>();
            Pricings = new HashSet<Pricing>();
            //StandardQuotations = new HashSet<StandardQuotation>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        [Display(Name = "Construction Type")]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }

        //public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
        public virtual ICollection<Pricing> Pricings { get; set; }
        //public virtual ICollection<StandardQuotation> StandardQuotations { get; set; }
    }
}
