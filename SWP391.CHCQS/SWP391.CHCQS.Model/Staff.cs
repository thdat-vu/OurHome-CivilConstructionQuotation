using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [MaxLength(12), MinLength(9)]
        public string? PhoneNum { get; set; }
        [MaxLength(30)]
        public string? Email { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; } = null!;
        [MaxLength(30)]
        public string Username { get; set; } = null!;
        [MaxLength(10)]
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
