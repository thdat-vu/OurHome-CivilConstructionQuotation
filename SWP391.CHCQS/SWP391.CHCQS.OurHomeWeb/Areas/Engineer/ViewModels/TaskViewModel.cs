namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public class TaskViewModel
	{
		public string Id { get; set; } = null!;
		public string Name { get; set; } = null!;
		public string? Description { get; set; }
		public decimal UnitPrice { get; set; }
		public bool Status { get; set; }
		public string CategoryId { get; set; } = null!;
		public string CategoryName { get; set; }
	}
}
