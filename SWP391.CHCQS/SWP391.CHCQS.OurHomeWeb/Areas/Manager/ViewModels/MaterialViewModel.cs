namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class MaterialViewModel
    {
        public MaterialViewModel(string id, string name, int inventoryQuantity, decimal unitPrice, string unit, bool status, string categoryId, string categoryName)
        {
            Id = id;
            Name = name;
            InventoryQuantity = inventoryQuantity;
            UnitPrice = unitPrice;
            Unit = unit;
            Status = status;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
        public MaterialViewModel()
        {
            
        }
    }
}
