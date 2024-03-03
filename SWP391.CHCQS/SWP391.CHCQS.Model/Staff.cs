using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class Staff : ApplicationUser
    {
        public Staff()
        {
            //CustomQuotationEngineers = new HashSet<CustomQuotation>();
            //CustomQuotationManagers = new HashSet<CustomQuotation>();
            //CustomQuotationSellers = new HashSet<CustomQuotation>();
            //InverseManager = new HashSet<Staff>();
        }        
        public string? ManagerId { get; set; } = null!;
        [ForeignKey("ManagerId")]		
        
		public virtual Staff? Manager { get; set; } = null!;
  //      public virtual ICollection<CustomQuotation>? CustomQuotationEngineers { get; set; } = null!;
  //      public virtual ICollection<CustomQuotation>? CustomQuotationManagers { get; set; } = null!;
		//public virtual ICollection<CustomQuotation>? CustomQuotationSellers { get; set; } = null!;
		//public virtual ICollection<Staff>? InverseManager { get; set; } = null!;
	}
}
