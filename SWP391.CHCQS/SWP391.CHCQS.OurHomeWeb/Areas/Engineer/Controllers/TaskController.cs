using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class TaskController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public TaskController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region
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
	}
}
