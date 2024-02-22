using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class MaterialController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		//Khai bao Session cho MaterialList neu co thi lay ra khong co thi tao moi
		public List<MaterialDetailViewModel> MaterialListSession => HttpContext.Session.Get<List<MaterialDetailViewModel>>(SessionConst.MATERIAL_LIST_KEY) ?? new List<MaterialDetailViewModel>();

		public MaterialController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		

		#region API CALL LIST MATERIAL
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


		public async Task<IActionResult> AddToQuote(string MaterialId)
		{
			return RedirectToAction("");
		}
	}
}
