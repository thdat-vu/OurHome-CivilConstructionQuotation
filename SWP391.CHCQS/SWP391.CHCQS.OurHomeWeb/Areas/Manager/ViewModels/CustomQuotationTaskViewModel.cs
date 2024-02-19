namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public partial class CustomQuotationTaskViewModel
    {
        //public TaskViewModel Task { get; set; }
        //public decimal Price { get; set; }
        public CustomQuotationTaskViewModel(TaskViewModel task, decimal price)
        {
            Task = task;
            Price = price;
        }
        public CustomQuotationTaskViewModel()
        {
            
        }

    }
}
