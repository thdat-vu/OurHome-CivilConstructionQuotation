using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Material
    {
        public Material()
        {
            //MaterialDetails = new HashSet<MaterialDetail>();
            Combos = new HashSet<Combo>();            
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [MaxLength(30)]
        public string Unit { get; set; } = null!;
        public bool Status { get; set; }
        [MaxLength(10)]
        [Display(Name = "Category Type")]
        public string CategoryId { get; set; } = null!;
        [ForeignKey("CategoryId")]
        public virtual MaterialCategory Category { get; set; } = null!;
        //public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }
        [MaxLength(200)]
        public string? ImageUrl { get; set; }
        public virtual ICollection<Combo> Combos { get; set; }        

    }
}
