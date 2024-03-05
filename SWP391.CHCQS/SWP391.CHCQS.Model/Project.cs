using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Project
    {
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(200)]
        public string Location { get; set; } = null!;
        [MaxLength(200)]
        public string Scale { get; set; } = null!;
        [MaxLength(100)]
        public string Size { get; set; } = null!;
        [MaxLength(500)]
        public string? Description { get; set; }

        public string? Overview { get; set; }        
        public bool Status { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; } = null!;
        [ForeignKey("CustomerId")]
        public virtual ApplicationUser Customer { get; set; } = null!;

        public virtual ICollection<ProjectImage> Images { get; set; }
    }
}
