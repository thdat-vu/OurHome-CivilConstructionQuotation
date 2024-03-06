using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class RequestVM
	{        
		public string RequestId { get; set; }

		public int NumberOfOrder { get; set; }
		public string? GenerateDate { get; set; }
		
		public string? Description { get; set; }
		
		public string? ConstructType { get; set; }
		
		public string? Acreage { get; set; }
		
		public string Location { get; set; } = null!;

		public string Status { get; set; }

		public IEnumerable<SelectListItem> ConstructionTypes { get; set; }
    }
}
