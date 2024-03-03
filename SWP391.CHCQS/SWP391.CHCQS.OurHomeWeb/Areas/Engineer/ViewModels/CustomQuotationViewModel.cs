namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public partial class CustomQuotationListViewModel
	{
		public string Id { get; set; } = null!;
		public DateTime? Date { get; set; }
		public string? Acreage { get; set; }
		public string Location { get; set; } = null!;
		public string Status { get; set; } = null!;
	}
}
