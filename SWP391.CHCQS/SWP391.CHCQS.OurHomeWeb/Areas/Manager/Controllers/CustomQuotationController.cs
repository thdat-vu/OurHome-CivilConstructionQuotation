using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
	/// <summary>
	/// This controller contains methods retrieving necessary data for Custom Quotation
	/// </summary>
	[Area("Manager")]
	public class CustomQuotationController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CustomQuotationController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{

			return View();
		}
		/// <summary>
		/// This method returns all pending approval CustomQuotation via CustomQuotationViewModel
		/// </summary>
		/// <returns>CustomQuotationViewModel</returns>
		[HttpGet]
		public IActionResult GetAll()
		{
			List<CustomQuotationViewModel> customQuotationViewModels = _unitOfWork.CustomQuotation
				.GetAll(includeProperties: "Engineer,Manager,Seller,ConstructDetail,Request")
				.Where(x => x.Status == SD.Pending_Approval)
				.Select(x => new CustomQuotationViewModel
				{
					Id = x.Id,
					Date = x.Date,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = SD.GetQuotationStatusDescription(x.Status),
					EngineerName = x.Engineer.Name,
					SellerName = x.Seller.Name,
					ManagerName = x.Manager.Name,
					ConstrucType = _unitOfWork.ConstructDetail.GetConstructTypeName(x.ConstructDetail.ConstructionId),
					GeneratRequestDate = x.Request.GenerateDate
				}).ToList();
			//TODO: Test
			//Console.WriteLine(customQuotationViewModels);
			return Json(new { data = customQuotationViewModels });
		}

		[HttpGet]
		public IActionResult GetDetail([FromQuery]string id)
		{
			var customQuotationDetail = _unitOfWork.CustomQuotation.GetById(id, "Manager,Engineer,Seller");
			//TODO: test result of custom quotation
			//return Json(new { data = customQuotationDetail });
			return View(customQuotationDetail);
		}
	}


}
