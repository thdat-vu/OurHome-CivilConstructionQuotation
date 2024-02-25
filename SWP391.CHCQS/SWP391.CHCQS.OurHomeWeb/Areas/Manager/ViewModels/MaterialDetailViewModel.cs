namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class MaterialDetailViewModel
    {
        public string? Id { get; set; }
        public string? MaterialName { get; set; }
        public int InventoryQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; }
        public bool Status { private get; set; }
        public string? StatusStr { get => Status ? "Yes" : "No"; }
        public string? CategoryName { get; set; }
    }
}
