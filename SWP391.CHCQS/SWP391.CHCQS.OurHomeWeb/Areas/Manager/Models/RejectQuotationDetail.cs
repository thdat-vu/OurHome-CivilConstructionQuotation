using SWP391.CHCQS.OurHomeWeb.Models;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models
{
    public class RejectQuotationDetail
    {
        public decimal? Total;
        public Dictionary<string, string>? TaskDetailNotes { get; set; } = null!;
        public Dictionary<string,MaterialNote>? MaterialDetailNotes { get; set; } = null!;

    }
}