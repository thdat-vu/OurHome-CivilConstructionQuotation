using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class RequestVM
	{
        public RequestForm RequestForm { get; set; }

		public IEnumerable<SelectListItem> ConstructionTypes { get; set; }
    }
}
