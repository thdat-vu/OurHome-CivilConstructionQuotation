using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class BasementType
    {
        public BasementType()
        {
            //ConstructDetails = new HashSet<ConstructDetail>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public decimal AreaFactor { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }

        //public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
    }
}
