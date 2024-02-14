using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class MaterialCategory
    {
        public MaterialCategory()
        {
            Materials = new HashSet<Material>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
    }
}
