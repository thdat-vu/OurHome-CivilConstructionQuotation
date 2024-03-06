using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Models;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels
{
    public class MaterialDetailListViewModel
    {
        public string? QuoteId { get; set; }
        public string? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public string? Unit { get; set; }
        public string? MaterialCateName { get; set; }
        public double? Quantity { get; set; }
        public decimal? Price { get; set; }
        public KeyValuePair<string, MaterialNote> Note { get; set; }
    }
}
