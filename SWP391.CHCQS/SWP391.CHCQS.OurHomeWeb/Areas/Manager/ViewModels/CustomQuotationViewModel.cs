
namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels
{
    public partial class CustomQuotationViewModel
    {
        //tên những staff dc ủy quyền xử lý custom quotation
        public string? EngineerName { get; set; }
        public string? SellerName { get; set; }
		public string? ManagerName { get; set; }
        //loại nhà dc thi công
        public string? ConstrucType { get; set; }
        //ngày request form dc tạo
        public DateTime GeneratRequestDate { get; set; }
	}
}
