using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class ConstructDetail
    {

        [Key]
        [MaxLength(10)]
        public string QuotationId { get; set; } = null!;
        [Required]
        public decimal Width { get; set; }
        [Required]
        public decimal Length { get; set; }
        [Required]
        public int Facade { get; set; }
        [MaxLength(50)]
        [Required]
        public string Alley { get; set; } = null!;
        [Required]
        [Range(1, 100)]
        public int Floor { get; set; }
        [Required]
        [Range(1, 100)]
        public int Room { get; set; }
        [Required]
        public decimal Mezzanine { get; set; }
        [Required]
        public decimal RooftopFloor { get; set; }
        public bool Balcony { get; set; }
        [Required]
        public decimal Garden { get; set; }
        [MaxLength(10)]
        public string ConstructionId { get; set; } = null!;
        [MaxLength(10)]
        public string InvestmentId { get; set; } = null!;
        [MaxLength(10)]
        public string FoundationId { get; set; } = null!;
        [MaxLength(10)]
        public string RooftopId { get; set; } = null!;
        [MaxLength(10)]
        public string BasementId { get; set; } = null!;

        public virtual BasementType Basement { get; set; } = null!;
        public virtual ConstructionType Construction { get; set; } = null!;
        public virtual FoundationType Foundation { get; set; } = null!;
        public virtual InvestmentType Investment { get; set; } = null!;
        public virtual CustomQuotation? Quotation { get; set; } = null!;
        public virtual RooftopType Rooftop { get; set; } = null!;

    }
}
