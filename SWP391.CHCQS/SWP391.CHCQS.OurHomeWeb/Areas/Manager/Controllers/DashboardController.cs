using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //[XuanDat]   
    //Mục tiêu của controller: tập hợp dữ liệu thống kê vào 1 nơi để dễ dàng truy xuất
    [Area("Manager")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //[XuanDat]
        //Dashboard theo Quote sẽ có 3 bar: Requests, Quotation, Cancled Request
        //hàm sẽ trả về đối tượng QuoteSummary theo kiểu JSon
        [HttpGet]
        public IActionResult GetQuoteSummary()
        {
            var list = new List<QuoteSummary>();
            var quoteSummary = new QuoteSummary()
            {
                //tổng số request
                Request = _unitOfWork.RequestForm.CountRequestSum(),
                //tổng số quotation được tạo
                CustomQuotation = _unitOfWork.CustomQuotation.CountCustomQuotationSum(),
                //đếm tổng số request đã bị cancle
                CancledRequest = _unitOfWork.RequestForm.CountCancleRequestSum()
            };
            var quoteSummary2 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary3 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary4 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary5 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary6 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary7 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            var quoteSummary8 = new QuoteSummary()
            {
                //tổng số request
                Request = 50,
                //tổng số quotation được tạo
                CustomQuotation = 70,
                //đếm tổng số request đã bị cancle
                CancledRequest = 10
            };
            list.Add(quoteSummary);
            list.Add(quoteSummary2);
            list.Add(quoteSummary3);
            list.Add(quoteSummary4);
            list.Add(quoteSummary5);
            list.Add(quoteSummary6);
            list.Add(quoteSummary7);
            list.Add(quoteSummary8);

            return Json(new { quoteStatistic = list});
        }
    }
}
