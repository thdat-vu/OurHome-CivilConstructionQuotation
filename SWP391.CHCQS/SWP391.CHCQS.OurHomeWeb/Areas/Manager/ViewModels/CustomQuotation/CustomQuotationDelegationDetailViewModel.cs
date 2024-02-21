using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels.CustomQuotation
{
    //Class Viewmodel
    public class CustomQuotationDelegationDetailViewModel
    {
		public string Id { get; set; } = null!;
		public string SellerId { get; set; } = null!;
		public string EngineerId { get; set; } = null!;
		public string ManagerId { get; set; } = null!;
		public DateTime? DelegationDateSeller { get; set; }
        public DateTime? SubmissionDateSeller { get; set; }
        public DateTime? RecieveDateEngineer { get; set; }
        public DateTime? SubmissionDateEngineer { get; set; }
        public DateTime? RecieveDateManager { get; set; }
        public DateTime? AcceptanceDateManager { get; set; }
    }
}
