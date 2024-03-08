using System.Runtime.CompilerServices;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class OverViewQuotationViewModel
    {
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public string Acreage { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
