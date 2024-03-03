using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class StandardQuotation
    {
        public StandardQuotation()
        {
            Materials = new HashSet<Material>();
            Tasks = new HashSet<Task>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string ConstructionId { get; set; } = null!;

        public virtual ConstructionType Construction { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
