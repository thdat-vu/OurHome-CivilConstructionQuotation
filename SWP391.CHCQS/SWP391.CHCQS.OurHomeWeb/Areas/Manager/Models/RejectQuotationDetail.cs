namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models
{
    public class RejectQuotationDetail
    {
        public decimal? Total;
        public IEnumerable<string>? CustomQuotaionTasks { get; set; } = null!;
        public Dictionary<string, int>? MaterialDetails { get; set; } = null!;
        public DateTime SubmissionDateEngineer { get; set; }
        public DateTime RecieveDateManager { get; set; } 
    }
}