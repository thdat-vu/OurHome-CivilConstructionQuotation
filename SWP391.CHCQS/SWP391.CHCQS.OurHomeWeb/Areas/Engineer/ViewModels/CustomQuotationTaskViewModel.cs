using SWP391.CHCQS.Model;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public partial class CustomQuotationTaskViewModel
	{
		public Task Task { get; set; }
		public string QuotationId { get; set; }
		public decimal Price { get; set; }
	}
}
