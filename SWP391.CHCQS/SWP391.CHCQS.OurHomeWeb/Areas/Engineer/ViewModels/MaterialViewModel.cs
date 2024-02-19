namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public partial class MaterialViewModel
	{
		public string Id { get; set; } = null!;
		public string Name { get; set; } = null!;
		public int InventoryQuantity { get; set; }
		public decimal UnitPrice { get; set; }
		public string Unit { get; set; } = null!;
		public bool Status { get; set; }
		public string CategoryId { get; set; } = null!;
		public string CategoryName { get; set; }
	}
}
