using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    //class  ghi lại thời gian và người dc ủy quyền xử lý customquotation
    //Có mối quan hệ 1-1 với CustomeQuotation
    public class CustomQuotationDelegation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CustomQuotation")]
        public string? CustomQuotationId {  get; set; }
        public CustomQuotation? CustomeQuotation { get; set; }

        //thời gian dc giao request và hoàn thành điền quotation của Seller
        public DateTime? DelegationDateSeller { get; set; }
        public DateTime? SubmissionDateSeller { get; set; }
        [ForeignKey("Staff")]
        public string? SellerId { get; set; } = null!;
        public virtual Staff? Seller { get; set; } = null!;

        //thời gian nhận customquotation và hoàn thành (MaterialDetail + CustomQuotationTask) của engineer
        public DateTime? RecieveDateEngineer { get; set; }
        public DateTime? SubmissionDateEngineer { get; set; }
        [ForeignKey("Staff")]
        public string? EngineerId { get; set; } = null!;
        public virtual Staff? Engineer { get; set; } = null!;

        //thời gian nhận customquotation đầy đủ và chấp nhận của Manager
        public DateTime? RecieveDateManager { get; set; }
        public DateTime? AcceptanceDateManager { get; set; }
        [ForeignKey("Staff")]
        public string? ManagerId { get; set; } = null!;
        public virtual Staff? Manager { get; set; } = null!;
    }
}
