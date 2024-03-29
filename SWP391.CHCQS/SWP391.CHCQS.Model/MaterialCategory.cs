using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> Demostration

namespace SWP391.CHCQS.Model
{
    public partial class MaterialCategory
    {
        public MaterialCategory()
        {
            
            Materials = new HashSet<Material>();
        }

        public string Id { get; set; } = null!;
<<<<<<< HEAD
=======
        [MaxLength(100)]
        [Display(Name = "Category Type")]
>>>>>>> Demostration
        public string Name { get; set; } = null!;
        [NotMapped]
        public virtual ICollection<Material> Materials { get; set; }
    }
}
