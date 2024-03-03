using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Staff
    {
        public Staff()
        {
            //CustomQuotationEngineers = new HashSet<CustomQuotation>();
            //CustomQuotationManagers = new HashSet<CustomQuotation>();
            //CustomQuotationSellers = new HashSet<CustomQuotation>();
            //InverseManager = new HashSet<Staff>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNum { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? ManagerId { get; set; } = null!;
		public bool Status { get; set; }

		public virtual Staff? Manager { get; set; } = null!;
		public virtual Account? UsernameNavigation { get; set; } = null!;
  //      public virtual ICollection<CustomQuotation>? CustomQuotationEngineers { get; set; } = null!;
  //      public virtual ICollection<CustomQuotation>? CustomQuotationManagers { get; set; } = null!;
		//public virtual ICollection<CustomQuotation>? CustomQuotationSellers { get; set; } = null!;
		//public virtual ICollection<Staff>? InverseManager { get; set; } = null!;
	}
}
