using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

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

        public async Task<IActionResult> GetDetail()
        {
            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString("quoteId");
            var taskDetail = _unitOfWork.CustomQuotaionTask.GetTaskDetail(quoteId)
                .Select((x) => new ViewModels.CustomQuotationTaskViewModel
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
