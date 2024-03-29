using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class QuotationVM
	{
		public CustomQuotation Quotation { get; set; } = null!;

		public ConstructDetail ConstructDetail { get; set; } = null!;
		public RequestForm Request { get; set; } = null!;

		public List<MaterialDetail> Materials { get; set; } = null!;
		public List<TaskDetail> Tasks { get; set; } = null!;
	}
}
