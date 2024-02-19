namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class MaterialDetailViewModel
    {
        public MaterialDetailViewModel(MaterialViewModel material, int quantity, decimal? price)
        {
            Material = material;
            Quantity = quantity;
            Price = price;
        }
        public MaterialDetailViewModel()
        {
            
        }
    }
}
