using SWP391.CHCQS.Model;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class CustomQuotationTaskViewModel
    {
        public CustomQuotationTaskViewModel(Task task, decimal price)
        {
            Task = task;
            Price = price;
        }
        public CustomQuotationTaskViewModel()
        {
            
        }
    }
}
