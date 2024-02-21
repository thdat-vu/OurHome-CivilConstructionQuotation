using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
	public partial class Staff
	{
		public Staff()
		{
			
		}

		public string Id { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? PhoneNum { get; set; }
		public string? Email { get; set; }
		public string Gender { get; set; } = null!;
		public string Username { get; set; } = null!;
		public string? ManagerId { get; set; }
		public bool Status { get; set; }

		public virtual Staff? Manager { get; set; }
	
		
	}
}
