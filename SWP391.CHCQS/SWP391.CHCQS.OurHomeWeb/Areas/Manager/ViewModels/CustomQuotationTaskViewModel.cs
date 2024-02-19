namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class CustomQuotationTaskViewModel
    {
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
