namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class CustomQuotationViewModel
	{
		public string Id { get; } = null!;
		public DateTime Date { get; }
		public string? Acreage { get; }
		public string Location { get; } = null!;
		public bool Status { get; }
		public string? Description { get; }
		public decimal Total { get; }
		public string SellerId { get; } = null!;
		public string SellerName { get; }
		public string EngineerId { get; } = null!;
		public string EngineerName { get; }
		public string ManagerId { get; } = null!;
		public string ManagerName { get; }
		public string RequestId { get; } = null!;
		public ConstructDetailViewModel ConstructDetail { get; }
		public ICollection<CustomQuotationTaskViewModel> CustomQuotationTask { get; set; }
		public ICollection<MaterialDetailViewModel> MaterialDetail { get; set; }

	}
}
