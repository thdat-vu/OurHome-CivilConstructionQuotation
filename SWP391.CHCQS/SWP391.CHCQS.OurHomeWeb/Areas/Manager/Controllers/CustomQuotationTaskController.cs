using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        #region GỌI API
        /// <summary>
        /// Action để gọi API trả về task detail
        /// Các bước thực hiện:
        /// 1. lấy quoteId đã được lưu vào session, đã dc lưu ghi người dùng GetDetail của Customquotation
        /// 2. Lấy note dc lưu trong session và file
        /// 3. Tiến hành đưa cho ViewModel trả về Json
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDetail()
        {
            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);

            //lấy note trong session
            var rejectDetail = GetRejectQuotationDetailFromSessionAndFile();

            var taskNote = rejectDetail.TaskDetailNotes;
            List<TaskDetailListViewModel> taskDetailVM = _unitOfWork.TaskDetail.GetTaskDetail(quoteId)
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
        #endregion
    }

}
