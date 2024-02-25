namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public partial class ConstructDetailViewModel
	{
		public string QuotationId { get; set; } = null!;
		public decimal Width { get; set; }
		public decimal Length { get; set; }
		public int Facade { get; set; }
		public string Alley { get; set; } = null!;
		public int Floor { get; set; }
		public int Room { get; set; }
		public decimal Mezzanine { get; set; }
		public decimal RooftopFloor { get; set; }
		public bool Balcony { get; set; }
		public decimal Garden { get; set; }
		public string ConstructionTypeName { get; set; } = null!;
		public string InvestmentTypeName { get; set; } = null!;
		public string FoundationTypeName { get; set; } = null!;
		public string RooftopTypeName { get; set; } = null!;
		public string BasementTypeName { get; set; } = null!;
	}
}
