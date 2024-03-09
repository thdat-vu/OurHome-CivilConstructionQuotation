using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public partial class MaterialDetailViewModel
	{
		public Material Material { get; set; }
		public string QuotationId { get; set; }
		public int Quantity { get; set; } = 1;
		public decimal? Price { get; set; }
	}
}
 