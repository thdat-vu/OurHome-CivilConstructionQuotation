namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class ComboDetailViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price {  get; set; }
        public bool Status { get; set; }
        public string ConstructionId { get; set; } = null!;
    }
}
