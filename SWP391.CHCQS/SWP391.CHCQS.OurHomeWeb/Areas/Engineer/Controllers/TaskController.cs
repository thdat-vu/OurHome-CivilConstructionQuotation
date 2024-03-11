using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Runtime.CompilerServices;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
    [Authorize(Roles = SD.Role_Engineer)]
    public class TaskController : Controller
	{


        #region ============ DECLARE ============


        private readonly IUnitOfWork _unitOfWork;

		//Declare session for CustomQuotationTaskViewModel to store TaskList of the quote when add into quote. if it empty, create one
		public List<TaskDetailViewModel> TaskListSession => HttpContext.Session.Get<List<TaskDetailViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<TaskDetailViewModel>();

		//Declare Session to store CustomQuotation serve to method AddToList in TaskController and MaterialController to add Task and Material.
		public CustomQuotationListViewModel CustomQuotationSession => HttpContext.Session.Get<CustomQuotationListViewModel>(SessionConst.CUSTOM_QUOTATION_KEY) ?? new CustomQuotationListViewModel();

		public TaskController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


        #endregion ============ DECLARE ============



        #region ============ API ============


        /// <summary>
        /// This function get all CustomeQuotation in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
			//Asign TaskListSession  for taskCart;
			var taskCart = TaskListSession;

			//Declare TaskVMlList
			List<TaskViewModel> TaskVMlList;
			//Get a list of all task from database and projection into TaskViewModel but not in TaskListSession
			//When a Task has been add into TaskListSession its will not appear in datatables
			TaskVMlList = _unitOfWork.Task
				.GetAll(includeProperties: "Category")
				.Where(t => t.Status == true && !TaskListSession.Any(ts => ts.Task.Id == t.Id))
				.Select(x => new TaskViewModel
				{
					Id = x.Id,
					Name = x.Name,
					Description = x.Description,
					UnitPrice = x.UnitPrice,
					Status = x.Status,
					CategoryId = x.CategoryId,
					CategoryName = x.Category.Name
				})
				.ToList();

			//Return Json for datatables to read
			return Json(new { data = TaskVMlList });

		}

		[HttpGet]
		public async Task<IActionResult> GetTaskListSession()
		{
			return Json(new { data = TaskListSession.ToList() });
		}

		[HttpGet]
		public async Task<IActionResult> GetTaskListHistory()
		{
			List<TaskDetailViewModel> customQuotationTaskViewModels;
			customQuotationTaskViewModels = _unitOfWork.TaskDetail.GetTaskDetail(CustomQuotationSession.Id, includeProp: "Task")
				.Select(x => new TaskDetailViewModel
				{
					Task = x.Task,
					QuotationId = x.QuotationId,
					Price = x.Price,
				}).ToList();

			return Json(new { data = customQuotationTaskViewModels });
		}

		/// <summary>
		/// 
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


        

        /// <summary>
        /// This fuction will add a Task into CustomQuotationTask in session when input a TaskId
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        [HttpGet]
		public async Task<IActionResult> AddToQuote(string TaskId)
		{
			//Asign TaskListSession to taskCart
			var taskCart = TaskListSession;

			//Get CustomeQuotationTaskViewModel and asign to taskItem from taskCart
			var taskItem = taskCart.FirstOrDefault(x => x.Task.Id == TaskId);

			//If taskItem equal null mean the TaskItem not in Session (not in session)
			if (taskItem == null)
			{
				//Get a task from database by TaskId
				var task = _unitOfWork.Task.Get(x => x.Id == TaskId);

				//Check if that task not in database
				if (task == null)
				{
					////Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
					//TempData["Error"] = $"Task not found with Id = {TaskId}";

					////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
					//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

					//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
					return Json(new { success = false, message = $"Không tìm thấy công việc! Mã = {TaskId}" });

				}
				else //if it not equal null
				{
					var width = _unitOfWork.ConstructDetail.Get(x => x.QuotationId == CustomQuotationSession.Id).Width;
					var length = _unitOfWork.ConstructDetail.Get(x => x.QuotationId == CustomQuotationSession.Id).Length;
					var acreage = width * length;
					//Asign new CustomQuotationTaskViewModel with projection from task for taskItem
					taskItem = new TaskDetailViewModel
					{
						Task = task,
						QuotationId = CustomQuotationSession.Id,
						Price = task.UnitPrice * acreage,
					};

					//Add taskItem into taskCart
					taskCart.Add(taskItem);
				}
			}
			else // if it already in session
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				//TempData["Error"] = $"Task already in quote with Id = {TaskId}";

				////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Công việc đã tồn tại! Mã = {TaskId}" });
			}

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			////Return success message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
			//TempData["Success"] = $"Add task successfully with Id = {TaskId}";

			////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Thêm công việc thành công! Mã = {TaskId}" });
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="TaskId"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteFromQuote(string TaskId)
		{
			//Asign TaskListSession to taskCart
			var taskCart = TaskListSession;

			//Get taskItem which exist in taskCart
			var taskItem = taskCart.Where(x => x.Task.Id == TaskId).FirstOrDefault();

			//if taskItem not in taskCart
			if (taskItem == null)
			{
				////Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				//TempData["Error"] = $"Task not found with Id = {TaskId}";

				////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Không tìm thấy công việc! Mã = {TaskId}" });
			}

			//Delete taskItem in taskCart
			taskCart.Remove(taskItem);

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			////Return success message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
			//TempData["Success"] = $"Delete task successfully with Id = {TaskId}";

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Xóa công việc thành công! Mã = {TaskId}" });
		}


        #endregion ============ API ============


    }
}
