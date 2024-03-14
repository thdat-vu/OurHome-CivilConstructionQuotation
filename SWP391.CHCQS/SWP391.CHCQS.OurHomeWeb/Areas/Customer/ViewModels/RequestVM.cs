using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class RequestVM
	{        
		public string RequestId { get; set; } = null!;

		public int NumberOfOrder { get; set; }
		public string? GenerateDate { get; set; }
		
		public string? Description { get; set; }
		
		public string? ConstructType { get; set; }

		[Display(Name = "Diện tích")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		public string Acreage { get; set; } = null!;
		[Display(Name = "Địa điểm")]
		[Required(ErrorMessage = "{0} không được bỏ trống")]
		public string Location { get; set; } = null!;

		public string Status { get; set; } = null!;

		public IEnumerable<SelectListItem> ConstructionTypes { get; set; }
    }
}
