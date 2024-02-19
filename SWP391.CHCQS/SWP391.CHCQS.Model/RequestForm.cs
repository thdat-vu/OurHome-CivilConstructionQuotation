using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class RequestForm
    {
        public RequestForm()
        {
            CustomQuotations = new HashSet<CustomQuotation>();
            Materials = new HashSet<Material>();
        }

        public string Id { get; set; } = null!;
        //thời gian tạo Request của khách hàng
        public DateTime GenerateDate { get; set; }
        public string? Description { get; set; }
        public string? ConstructType { get; set; }
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
        public bool Status { get; set; }
        public string CustomerId { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<CustomQuotation> CustomQuotations { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
