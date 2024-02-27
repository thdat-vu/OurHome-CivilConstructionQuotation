namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models
{
    public class RejectQuotationDetail
    {
        public decimal? Total;
        public IEnumerable<string>? CustomQuotaionTasks { get; set; } = null!;
        public IEnumerable<string>? MaterialDetails { get; set; } = null!;
        public string? SubmissionDateEngineer { get; set; } = null!;
        public string? RecieveDateManager { get; set; } = null!;
    }
}