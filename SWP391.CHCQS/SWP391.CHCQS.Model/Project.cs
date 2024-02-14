using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Project
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Scale { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string? Description { get; set; }
        public bool Status { get; set; }
        public string CustomerId { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
    }
}
