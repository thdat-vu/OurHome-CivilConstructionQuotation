using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
	public partial class RooftopType
	{
		public RooftopType()
		{
			//ConstructDetails = new HashSet<ConstructDetail>();
		}
		[MaxLength(10)]
		public string Id { get; set; } = null!;
		[MaxLength(30)]
		public string Name { get; set; } = null!;
		public decimal AreaFactor { get; set; }
		[MaxLength(500)]
		public string? Description { get; set; }

		//public virtual ICollection<ConstructDetail> ConstructDetails { get; set; }
	}
}
