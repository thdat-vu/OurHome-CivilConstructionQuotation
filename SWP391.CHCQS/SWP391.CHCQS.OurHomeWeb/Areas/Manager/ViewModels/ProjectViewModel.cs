using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class ProjectViewModel
	{
		public Project Project { get; set; }
		
		[ValidateNever]
		public IEnumerable<SelectListItem> CustomerList { get; set; }
	}
}
