using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomQuotationTaskController : BaseController
    {
        public CustomQuotationTaskController(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : base(unitOfWork, environment)
        {

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



            //lấy note trong session
            var rejectDetail = GetRejectQuotationDetailFromSessionAndFile();

            var taskNote = rejectDetail.TaskDetailNotes;

            List<TaskDetailListViewModel>  taskDetailVM = _unitOfWork.TaskDetail.GetTaskDetail(quoteId)
                .Select((x) => new ViewModels.TaskDetailListViewModel
                {
                    QuoteId = x.QuotationId,
                    TaskId = x.TaskId,
                    TaskName = _unitOfWork.Task.GetName(x.TaskId),
                    Price = x.Price,
                    Note = new KeyValuePair<string, string>(x.TaskId, taskNote[x.TaskId])
                }).ToList();
            
            return Json(new { data = taskDetailVM });
        }

    }

}
