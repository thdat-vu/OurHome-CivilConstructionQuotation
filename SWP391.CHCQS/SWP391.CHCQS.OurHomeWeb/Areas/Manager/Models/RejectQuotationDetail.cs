using SWP391.CHCQS.OurHomeWeb.Models;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models
{
    public class RejectQuotationDetail
    {
<<<<<<< HEAD
        public decimal? Total;
        public IEnumerable<string>? CustomQuotaionTasks { get; set; } = null!;
        public Dictionary<string, int>? MaterialDetails { get; set; } = null!;
        public DateTime? SubmissionDateEngineer { get; set; }
        public DateTime? RecieveDateManager { get; set; } 
=======
        public decimal? Total { get; set; }
        public Dictionary<string, string>? TaskDetailNotes { get; set; }
        public Dictionary<string,MaterialNote>? MaterialDetailNotes { get; set; } 

>>>>>>> Demostration
    }
}