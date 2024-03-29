using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels
{
	public class ConstructDetailViewModel
	{
        public ConstructDetail ConstructDetail { get; set; } = null!;
		[ValidateNever]
		public RequestForm Request { get; set; } = null!;

		[ValidateNever]
		public IEnumerable<SelectListItem> Basement { get; set; }

		[ValidateNever]
        public IEnumerable<SelectListItem> Construction { get; set; }
		[ValidateNever]
        public IEnumerable<SelectListItem> Foundation { get; set; }
		[ValidateNever]
        public IEnumerable<SelectListItem> Investment { get; set; }

		[ValidateNever]
        public IEnumerable<SelectListItem> Rooftop { get; set; }

		[ValidateNever]
		public List<SelectListItem> Alleys { get; set; }
        [ValidateNever]
        public List<SelectListItem> Facades { get; set; }
    }
}
