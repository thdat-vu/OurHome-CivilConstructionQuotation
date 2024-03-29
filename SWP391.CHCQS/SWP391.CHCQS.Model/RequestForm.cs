using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class RequestForm
    {
        public RequestForm()
        {
            
            Materials = new HashSet<Material>();
        }

        public string Id { get; set; } = null!;
        public DateTime GenerateDate { get; set; }
        public string? Description { get; set; }
        public string? ConstructType { get; set; }
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
<<<<<<< HEAD
        //true: vẫn dag xử lý, còn hiệu lực
        //false: ko còn hiệu lực
        public bool Status { get; set; }
        public string CustomerId { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        
=======
        

        [MaxLength(20)]
        public string Status { get; set; }

        public string CustomerId { get; set; } = null!;
        [ForeignKey("CustomerId")]                                        
        
        public virtual ApplicationUser Customer { get; set; } = null!;
>>>>>>> Demostration
        public CustomQuotation? CustomQuotation { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
