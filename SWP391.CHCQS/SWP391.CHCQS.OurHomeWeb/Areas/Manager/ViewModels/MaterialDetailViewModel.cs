namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public partial class MaterialDetailViewModel
    {
        //public MaterialViewModel Material { get; set; }
        //public int Quantity { get; set; }
        //public decimal? Price { get; set; }
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
