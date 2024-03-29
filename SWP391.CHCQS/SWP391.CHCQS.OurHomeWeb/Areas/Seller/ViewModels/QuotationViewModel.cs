using SWP391.CHCQS.Model;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels
{
    public class QuotationViewModel
    {

        public CustomQuotation CustomQuotation { get; set; }
        public ConstructDetail ConstructDetail { get; set; }

        public string ConstructionName { get; set; }
        public string InvestmentName { get; set; }
        public string FoundationName { get; set; }
        public string BasementName { get; set; }
        public string RoofName { get; set; }

        
	}

}
