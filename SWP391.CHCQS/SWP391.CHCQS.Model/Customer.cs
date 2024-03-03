using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
	public partial class Customer : ApplicationUser
	{
		public Customer()
		{
			Projects = new HashSet<Project>();
			RequestForms = new HashSet<RequestForm>();
		}
		

		public virtual ICollection<Project> Projects { get; set; }
		public virtual ICollection<RequestForm> RequestForms { get; set; }
	}
}
