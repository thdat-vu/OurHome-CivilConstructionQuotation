namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels
{
    public class QuotationStatusViewModel
    {
        public string Id { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
