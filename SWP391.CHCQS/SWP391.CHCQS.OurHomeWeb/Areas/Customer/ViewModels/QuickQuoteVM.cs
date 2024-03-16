using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class QuickQuoteVM
	{
        public ConstructDetail ConstructDetail { get; set; } = null!;
		[ValidateNever]
		public List<SelectListItem> Alleys { get; set; }
		[ValidateNever]
		public List<SelectListItem> Facades { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> ConstructionTypes { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> InvestmentTypes { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> FoundationTypes { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> RooftopTypes { get; set; }
		[ValidateNever]
		public IEnumerable<SelectListItem> BasementTypes { get; set; }
        public Bill? ResponseBill { get; set; }

    }
}
