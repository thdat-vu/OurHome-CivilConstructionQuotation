using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class MaterialCategory
    {
        public MaterialCategory()
        {
            //
            //Materials = new HashSet<Material>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        [Display(Name = "Category Type")]
        public string Name { get; set; } = null!;

        //public virtual ICollection<Material> Materials { get; set; }
    }
}
