using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomQuotationTaskController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomQuotationTaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        //hàm trả về task detail theo quotation mà Manager đang coi
        //cụ thể là hàm lấy quoteId đã được lưu vào session - hành động này thực hiện ở GetDetail của CustomQuotationController
        //TaskDetailDTB sẽ gọi ajax đến đây để lấy dữ liệu 
        public IActionResult GetDetail()
        {
            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            var taskDetail = _unitOfWork.TaskDetail.GetTaskDetail(quoteId)
                .Select((x) => new ViewModels.TaskDetailListViewModel
                {
                    QuoteId = x.QuotationId,
                    TaskId = x.TaskId,
                    TaskName = _unitOfWork.Task.GetName(x.TaskId),
                    Price = x.Price

                }).ToList();
            return Json(new { data = taskDetail });
        }
        
    }

}
