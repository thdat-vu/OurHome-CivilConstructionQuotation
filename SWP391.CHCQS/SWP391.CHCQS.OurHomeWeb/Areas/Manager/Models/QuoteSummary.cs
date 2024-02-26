namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models
{
    //class chứa dữ liệu sau khi xử lý dành cho việc vẽ ra chart
    public class QuoteSummary
    {
        public int Request {  get; set; }
        public int CustomQuotation { get; set; }
        public int CancledRequest { get; set; }
        public string? Timeline { get; set; }
    }
}
