using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels
{
	public class QuotationVM
	{
		public string Id { get; set; }
		public string? ConstructionType { get; set; }
		public string? InvestmentType { get; set; }
		public string? FoundationType { get; set; }
		public string? RoofType { get; set; }
		public string? BasementType { get; set; }
		public decimal? Width { get; set; }
		public decimal? Lenght { get; set; }
		public int? Facade { get; set; }
		public string? Alley { get; set; }
		public int? Floor { get; set; }
		public decimal? Mezzanine { get; set; }
		public decimal? RooftopFloor { get; set; }
		public bool Balcony { get; set; }
		public decimal? Garden { get; set; }
		public string? Description { get; set; }
		public decimal TotalPrice { get; set; }

		public List<MaterialDetail> Materials { get; set; }
		public List<TaskDetail> Tasks { get; set; }
	}
}
