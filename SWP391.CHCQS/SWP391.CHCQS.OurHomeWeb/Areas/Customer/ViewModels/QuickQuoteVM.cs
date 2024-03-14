using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class QuickQuoteVM
	{
        public ConstructDetail ConstructDetail { get; set; } = null!;
		public List<SelectListItem> Alleys { get; set; }
		public List<SelectListItem> Facades { get; set; }
		public IEnumerable<SelectListItem> ConstructionTypes { get; set; }
		public IEnumerable<SelectListItem> InvestmentTypes { get; set; }
		public IEnumerable<SelectListItem> FoundationTypes { get; set; }
		public IEnumerable<SelectListItem> RooftopTypes { get; set; }
		public IEnumerable<SelectListItem> BasementTypes { get; set; }
        public Bill ResponseBill { get; set; }

    }
}
