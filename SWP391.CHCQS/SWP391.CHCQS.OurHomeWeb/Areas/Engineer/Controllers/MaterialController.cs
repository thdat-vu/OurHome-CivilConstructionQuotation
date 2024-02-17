using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class MaterialController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public MaterialController(IUnitOfWork unitOfWork)
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
			List<MaterialViewModel> materialVMList = _unitOfWork.Material
				.GetAll(includeProperties: "Category")
				.Where(m => m.Status == true)
				.Select(x => new MaterialViewModel
				{
					Id = x.Id,
					Name = x.Name,
					InventoryQuantity = x.InventoryQuantity,
					UnitPrice = x.UnitPrice,
					Unit = x.Unit,
					Status = x.Status,
					CategoryId = x.CategoryId,
					CategoryName = x.Category.Name
				})
				.ToList();

			return Json(new { data = materialVMList });
		}
		#endregion
	}
}
