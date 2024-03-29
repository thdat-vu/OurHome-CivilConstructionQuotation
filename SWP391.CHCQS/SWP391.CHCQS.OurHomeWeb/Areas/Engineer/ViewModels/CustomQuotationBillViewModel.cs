namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public class CustomQuotationBillViewModel
    {
        public decimal PriceOnAcreage { get; set; }
        public decimal Acreage { get; set; }
        public decimal FoundationAcreage { get; set; }
        public decimal BasementAcreage { get; set; }
        public decimal RooftopAcreage { get; set; }
        public decimal BalconyAcreage { get; set; }
        public decimal TotalAcreage { get; set; }
        public decimal TotalPriceTask { get; set; }
        public decimal TotalPriceMaterial { get; set; }
        public decimal TotalPrice {  get; set; }
    }
}
