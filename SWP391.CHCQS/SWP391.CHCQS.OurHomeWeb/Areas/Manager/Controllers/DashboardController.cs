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
        public IActionResult GetQuoteSummaryFilterByMonthAndYear(int year)
        {
            //tạo danh sách chứa đối tượng lưu data
            var list = new List<QuoteSummary>();
            //tạo mảng 12 tháng
            string[] timeLine = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            //vòng lặp để lưu thông tin vào 12 đối tượng QuoteSummary
            for (int i = 0; i < timeLine.Length; i++)
            {
                int month = i + 1;
                var quoteSummary = new QuoteSummary()
                {
                    
                    //tổng số request
                    Request = _unitOfWork.RequestForm.CountRequestInMonthAndYear(month, year),
                    //tổng số quotation được tạo
                    CustomQuotation = _unitOfWork.CustomQuotation.CountCustomQuotationInMonthAndYear(month, year),
                    //đếm tổng số request đã bị cancle
                    CancledRequest = _unitOfWork.RequestForm.CountCancledRequestInMonthAndYear(month, year),
                    //Gán timeline
                    Timeline = timeLine[i]
                };
                //thêm vào danh sách
                list.Add(quoteSummary);
            }
            return Json(new { quoteStatistic = list});
        }
        //function này sẽ thống kê tất cả các năm có request dc tạo -> nhằm tạo ra select option theo year cho chartjs
        //Hàm sẽ lọc theo table Request để lấy ra năm nào có request
        //ko cần lọc theo customquotation vì khi có request thì custom quotation cũng sẽ dc tạo, chậm nhất là trong 1 tiếng 
        [HttpGet]
        public IActionResult GetYearList()
        {
            var yearList = _unitOfWork.RequestForm.GetYearList();
            return Json(new { data = yearList });
        }
    }
}
