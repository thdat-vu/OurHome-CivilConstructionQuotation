using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

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
            //lấy note trong session, để load lại trang có note - nếu ko có thì tạo mới
            var rejectDetail = (HttpContext.Session.Get<RejectQuotationDetail>(quoteId));
            if (rejectDetail == null)
            {
                rejectDetail = new RejectQuotationDetail()
                {
                    MaterialDetailNotes = new Dictionary<string, MaterialNote>(),
                    TaskDetailNotes = new Dictionary<string, string>()
                };
            }
            List<TaskDetailListViewModel> taskDetailVM = null;
            var taskNote = rejectDetail.TaskDetailNotes;
            //lưu vào Session lại
            HttpContext.Session.Set(quoteId, rejectDetail);

            taskDetailVM = _unitOfWork.TaskDetail.GetTaskDetail(quoteId)
                .Select((x) => new ViewModels.TaskDetailListViewModel
                {
                    QuoteId = x.QuotationId,
                    TaskId = x.TaskId,
                    TaskName = _unitOfWork.Task.GetName(x.TaskId),
                    Price = x.Price,
                    Note = new KeyValuePair<string, string>(x.TaskId, taskNote.ContainsKey(x.TaskId) ? taskNote[x.TaskId] : "" )
                }).ToList();

            return Json(new { data = taskDetailVM });
        }

    }

}
