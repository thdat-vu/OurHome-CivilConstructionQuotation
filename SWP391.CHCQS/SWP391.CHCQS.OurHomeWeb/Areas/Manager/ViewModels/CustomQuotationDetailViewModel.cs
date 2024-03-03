using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
	public class CustomQuotationDetailViewModel
	{
		public string? QuoteId { get; set; }
		public string? RequestId { get; set; }
		public ConstructDetailViewModel? ConstructDetailVM { get; set; } 
		public DateTime? QuoteGeneratedDate { get; set; }

		public Staff? Enginneer { get; set; } = null!;
		public Staff? Manager { get; set; } = null!;
		public Staff? Seller { get; set; } = null!;
		public decimal Total {  get; set; }

		//ghi lại thời gian và người dc ủy quyền xử lý custom quotation
		//thời gian dc giao request và hoàn thành điền quotation của Seller
		public DateTime? DelegationDateSeller { get; set; } 
		public DateTime? SubmissionDateSeller { get; set; } 
		//thời gian nhận customquotation và hoàn thành (MaterialDetail + CustomQuotationTask) của engineer
		public DateTime? RecieveDateEngineer { get; set; } 
		public DateTime? SubmissionDateEngineer { get; set; } 
		//thời gian nhận customquotation đầy đủ và chấp nhận của Manager
		public DateTime? RecieveDateManager { get; set; } 
		public DateTime? AcceptanceDateManager { get; set; }
	}
}
