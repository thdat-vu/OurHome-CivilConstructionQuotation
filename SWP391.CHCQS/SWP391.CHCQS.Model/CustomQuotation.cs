using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class CustomQuotation
    {
        public CustomQuotation()
        {
            CustomQuotaionTasks = new HashSet<CustomQuotationTask>();
            MaterialDetails = new HashSet<MaterialDetail>();
        }

        public string Id { get; set; } = null!;
        public DateTime Date { get; set; }
        public string? Acreage { get; set; }
        public string Location { get; set; } = null!;
		//các trạng thái
		//seller nhận xử lý: preparing (1)
		    //TH bị hủy ở Seller: cancle (-1) - khách hàng ko nghe máy || ko thích tư vấn
		//engineer nhận xử lý: processing (2)
		//manager nhận:  Pending_Approval (3)
		//manager acceptance: Completed (4)
		    //TH manager reject: rejected (0) -> quay lại processing khi engineer nhận (2)
		public int Status { get; set; }
        public string? Description { get; set; }
        public decimal Total { get; set; }
 
        public string? SellerId { get; set; } = null!;

		public string? EngineerId { get; set; } = null!;

		public string? ManagerId { get; set; } = null!;


		public string? RequestId { get; set; } = null!;

        public virtual Staff? Engineer { get; set; } = null!;
        public virtual Staff? Manager { get; set; } = null!;
        public virtual RequestForm? Request { get; set; } = null!;
        public virtual Staff? Seller { get; set; } = null!;
        public virtual ConstructDetail? ConstructDetail { get; set; } = null!;

        public ICollection<CustomQuotationTask>? CustomQuotaionTasks { get; set; } = null!;
        public ICollection<MaterialDetail>? MaterialDetails { get; set; } = null!;

        //ghi lại thời gian và người dc ủy quyền xử lý custom quotation
        //thời gian dc giao request và hoàn thành điền quotation của Seller
        public DateTime? DelegationDateSeller { get; set; } = null!;
        public DateTime? SubmissionDateSeller { get; set; } = null!;
        //thời gian nhận customquotation và hoàn thành (MaterialDetail + CustomQuotationTask) của engineer
        public DateTime? RecieveDateEngineer { get; set; } = null!;
        public DateTime? SubmissionDateEngineer { get; set; } = null!;
        //thời gian nhận customquotation đầy đủ và chấp nhận của Manager
        public DateTime? RecieveDateManager { get; set; } = null!;
        public DateTime? AcceptanceDateManager { get; set; } = null!;
    }
}
