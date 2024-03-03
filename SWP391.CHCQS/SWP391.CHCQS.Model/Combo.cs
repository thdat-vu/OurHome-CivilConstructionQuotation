using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Combo
    {
        public Combo()
        {
            Materials = new HashSet<Material>();
            Tasks = new HashSet<Task>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        [MaxLength(10)]
        public string ConstructionId { get; set; } = null!;
        [ForeignKey("ConstructionId")]
        public virtual ConstructionType Construction { get; set; } = null!;

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        public string? ImageUrl { get; set; }
    }
}
