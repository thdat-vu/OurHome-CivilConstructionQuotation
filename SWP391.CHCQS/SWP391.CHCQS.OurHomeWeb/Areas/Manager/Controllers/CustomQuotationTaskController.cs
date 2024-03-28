using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
	[Authorize(Roles = SD.Role_Manager)]
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

        /// <summary>
		/// function to be called api for onclick event
		/// </summary>
		/// <param name="TaskId"></param>
		/// <returns></returns>
		[HttpGet]
        [ActionName("Detail")]
        public async Task<IActionResult> GetDetail([FromQuery] string TaskId)
        {
            var taskDetail = _unitOfWork.Task.Get((x) => x.Id == TaskId, "Category");
            var taskDetailVM = new TaskViewModel
            {
                Id = taskDetail.Id,
                Name = taskDetail.Name,
                Description = taskDetail.Description,
                UnitPrice = taskDetail.UnitPrice,
                Status = taskDetail.Status,
                CategoryId = taskDetail.CategoryId,
                CategoryName = taskDetail.Category.Name

            };
            //TODO: Test result
            return Json(new { data = taskDetailVM });
        }
        #endregion
    }

}
