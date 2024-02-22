using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class TaskController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		//Khai bao Session cho TaskList neu co thi lay ra khong co thi tao moi
		public List<CustomQuotationTaskViewModel> TaskListSession => HttpContext.Session.Get<List<CustomQuotationTaskViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<CustomQuotationTaskViewModel>();

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
			List<TaskViewModel> TaskVMlList = _unitOfWork.Task
				.GetAll(includeProperties: "Category")
				.Where(t => t.Status == true)
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

			return Json(new { data = TaskVMlList });
		}
		#endregion

		public async Task<IActionResult> AddToList(string TaskId)
		{
			var taskCart = TaskListSession;
			var taskItem = taskCart.FirstOrDefault(x => x.Task.Id == TaskId);

			if (taskItem == null)
			{
				var task = _unitOfWork.Task.Get(x => x.Id == TaskId);
				if (task == null)
				{
					TempData["Message"] = $"Task not found with Id = {TaskId}";
					return RedirectToAction("Quote", "Quotation");
				}
			}

			return RedirectToAction("");
		}

	}
}
