using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class RejectReportVM
    {
        //chứa id quotation detail bị reject
        public string RejectQuotationId { get; set; }
        //chứa id manager làm detail r nộp
        public string SubcriberId { get; set; }
        //chứa id manager đã reject
        public string RejecterId { get; set; }
        
        public string ManagerId { get; set; }

        public DateTime? RecieveManagerDate { get; set; }
        public string EngineerId { get; set; }
        public DateTime? SubmissionEngineerDate { get; set; }
        //chứa lý do của manager reject/ chỉnh sửa
        [Required(ErrorMessage = "Reason is required")]
        [MinLength(20, ErrorMessage = "The reason should be longer, upper 20 charaters")] //lý do nên có ít nhất 20 ký tự để thể hiện rõ ý 
        [MaxLength(500, ErrorMessage = "The reason should be shorter, under 500 charaters")] //lý do chỉ nên có nhiều nhất 500 ký tự
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
