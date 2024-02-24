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
	public class TaskController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		//Declare session for CustomQuotationTaskViewModel to store TaskList of the quote when add into quote. if it empty, create one
		public List<CustomQuotationTaskViewModel> TaskListSession => HttpContext.Session.Get<List<CustomQuotationTaskViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<CustomQuotationTaskViewModel>();

		//Declare Session to store CustomQuotation serve to method AddToList in TaskController and MaterialController to add Task and Material.
		public CustomQuotationListViewModel CustomQuotationSession => HttpContext.Session.Get<CustomQuotationListViewModel>(SessionConst.CUSTOM_QUOTATION_KEY) ?? new CustomQuotationListViewModel();

		public TaskController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region API CALL TASK LIST
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

			//if taskCart empty it will get data from database.
			if (taskCart.Count == 0)
			{
				//Get data from database to check
				List<CustomQuotationTask> customQuotationTasks = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id).ToList();

				TaskVMlList = _unitOfWork.Task
				   .GetAll(includeProperties: "Category")
				   .Where(t => t.Status == true && !customQuotationTasks.Any(ts => ts.Task.Id == t.Id))
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
			}
			else
			{
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
			}
			//Return Json for datatables to read
			return Json(new { data = TaskVMlList });

		}

		[HttpGet]
		public async Task<IActionResult> GetTaskListSession()
		{
			//Asign TaskListSession for taskCart;
			var taskCart = TaskListSession;

			//if taskCart == null mean the taskCart have no task in there
			if (taskCart.Count == 0)
			{
				taskCart = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id, includeProp: null).Select(x => new CustomQuotationTaskViewModel
				{
					Task = _unitOfWork.Task.Get(t => t.Id == x.TaskId),
					QuotationId = x.QuotationId,
					Price = x.Price,
				}).ToList();
			}

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			return Json(new { data = TaskListSession.ToList() });
		}

		#endregion

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
					//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
					TempData["Error"] = $"Task not found with Id = {TaskId}";

					////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
					//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

					//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
					return Json(new { success = false, message = $"Add task false with Id = {TaskId}" });

				}
				else //if it not equal null
				{
					//Asign new CustomQuotationTaskViewModel with projection from task for taskItem
					taskItem = new CustomQuotationTaskViewModel
					{
						Task = task,
						QuotationId = CustomQuotationSession.Id,
						Price = task.UnitPrice * 0.8m
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
				return Json(new { success = false, message = $"Add task false with Id = {TaskId}" });
			}

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			////Return success message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
			//TempData["Success"] = $"Add task successfully with Id = {TaskId}";

			////Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			//return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Add task successfully with Id = {TaskId}" });
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
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Task not found with Id = {TaskId}";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Delete taskItem in taskCart
			taskCart.Remove(taskItem);

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			////Return success message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
			//TempData["Success"] = $"Delete task successfully with Id = {TaskId}";

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Delete task successfully with Id = {TaskId}" });
		}

	}
}
