
namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class CustomQuotationViewModel
    {
        public CustomQuotationViewModel(string id, DateTime date, string? acreage, string location, string status)
        {
            Id = id;
            Date = date;
            Acreage = acreage;
            Location = location;
            Status = status;
        }
        public CustomQuotationViewModel()
        {
            
        }
    }
}
