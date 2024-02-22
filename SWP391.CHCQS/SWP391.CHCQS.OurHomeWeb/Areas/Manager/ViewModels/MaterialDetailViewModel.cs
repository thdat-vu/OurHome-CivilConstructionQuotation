using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class MaterialDetailViewModel
    {
        public MaterialDetailViewModel(Material material, int quantity, decimal? price)
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
