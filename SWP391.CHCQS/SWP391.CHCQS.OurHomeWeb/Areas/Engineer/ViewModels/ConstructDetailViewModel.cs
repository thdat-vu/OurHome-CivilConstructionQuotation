namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
	public class ConstructDetailViewModel
	{
		public string QuotationId { get; } = null!;
		public decimal Width { get; }
		public decimal Length { get; }
		public int Facade { get; }
		public string Alley { get; } = null!;
		public int Floor { get; }
		public int Room { get; }
		public decimal Mezzanine { get; }
		public decimal RooftopFloor { get; }
		public bool Balcony { get; }
		public decimal Garden { get; }
		public string ConstructionTypeName { get; } = null!;
		public string InvestmentTypeName { get; } = null!;
		public string FoundationTypeName { get; } = null!;
		public string RooftopTypeName { get; } = null!;
		public string BasementTypeName { get; } = null!;
	}
}
