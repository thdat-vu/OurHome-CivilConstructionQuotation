using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels
{
	public class ConstructDetailViewModel
	{
        [ValidateNever]
        public ConstructDetail ConstructDetail { get; set; }

		[Required]
		[ValidateNever]
		public IEnumerable<SelectListItem> Basement { get; set; }
		[Required]
		[ValidateNever]
        public IEnumerable<SelectListItem> Construction { get; set; }
		[ValidateNever]
        [Required]
        public IEnumerable<SelectListItem> Foundation { get; set; }
		[Required]
		[ValidateNever]
        public IEnumerable<SelectListItem> Investment { get; set; }
		[Required]
		[ValidateNever]
        public IEnumerable<SelectListItem> Rooftop { get; set; }
	}
}
