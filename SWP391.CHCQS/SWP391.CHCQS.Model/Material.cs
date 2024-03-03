using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Material
    {
        public Material()
        {
            //MaterialDetails = new HashSet<MaterialDetail>();
            Quotations = new HashSet<StandardQuotation>();
            Requests = new HashSet<RequestForm>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int InventoryQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = null!;
        public bool Status { get; set; }
        public string CategoryId { get; set; } = null!;
        
        public virtual MaterialCategory Category { get; set; } = null!;
        //public virtual ICollection<MaterialDetail> MaterialDetails { get; set; }

        public virtual ICollection<StandardQuotation> Quotations { get; set; }
        public virtual ICollection<RequestForm> Requests { get; set; }

    }
}
