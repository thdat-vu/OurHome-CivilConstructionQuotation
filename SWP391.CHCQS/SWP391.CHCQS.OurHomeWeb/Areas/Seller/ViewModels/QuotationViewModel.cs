namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels
{
    public class QuotationViewModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ConstructType { get; set; }
        public string Acreage { get; set; }
        public string Location { get; set; }
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
		public string ConstructionId { get; set; } = null!;
		public string InvestmentId { get; set; } = null!;
		public string FoundationId { get; set; } = null!;
		public string RooftopId { get; set; } = null!;
		public string BasementId { get; set; } = null!;
	}
}
