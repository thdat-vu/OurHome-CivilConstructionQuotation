namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public partial class MaterialViewModel
	{
        public MaterialViewModel()
        {
            
        }

        public MaterialViewModel(string id, string name, int inventoryQuantity, decimal unitPrice, string unit, bool status, string categoryId, string categoryName )
        {
            this.Id = id;
            this.Name = name;
            this.InventoryQuantity = inventoryQuantity;
            this.UnitPrice = unitPrice;
            this.Unit = unit;
            this.Status = status;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.Id = id;
            this.Name = name;
            this.InventoryQuantity = inventoryQuantity;
            this.UnitPrice = unitPrice;
            this.Unit = unit;
            this.Status = status;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
        }

        
	}
}
