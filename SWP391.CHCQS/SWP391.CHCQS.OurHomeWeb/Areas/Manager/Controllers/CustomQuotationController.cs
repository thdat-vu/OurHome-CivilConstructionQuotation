using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using System.Linq.Expressions;
using Task = SWP391.CHCQS.Model.Task;

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
			var customQuotationDetail = _unitOfWork.CustomQuotation.Get(x => x.Id == id, "Manager,Engineer,Seller,ConstructDetail");
			var customQuotationDetailVM = new CustomQuotationDetailViewMdel()
			{
				RequestId = customQuotationDetail.RequestId,
				ConstructDetailVM = new ViewModels.ConstructDetailViewModel(),
				QuoteGeneratedDate = customQuotationDetail.Date,
				Enginneer = customQuotationDetail.Engineer,
				Manager = customQuotationDetail.Manager,
				Seller = customQuotationDetail.Seller,
				DelegationDateSeller = customQuotationDetail.DelegationDateSeller,
				SubmissionDateSeller = customQuotationDetail.SubmissionDateEngineer,
				RecieveDateEngineer = customQuotationDetail.RecieveDateEngineer,
				SubmissionDateEngineer = customQuotationDetail.SubmissionDateEngineer,
				RecieveDateManager = customQuotationDetail.RecieveDateManager,
				AcceptanceDateManager = customQuotationDetail.AcceptanceDateManager
			};
			customQuotationDetailVM.ConstructDetailVM.IsBalcony = customQuotationDetail.ConstructDetail.Balcony;
			customQuotationDetailVM.ConstructDetailVM.TypeOfConstruct = _unitOfWork.ConstructionType.GetName(customQuotationDetail.ConstructDetail.ConstructionId);
			customQuotationDetailVM.ConstructDetailVM.Investment = _unitOfWork.InvestmentType.GetName(customQuotationDetail.ConstructDetail.InvestmentId);
			customQuotationDetailVM.ConstructDetailVM.Foundation = _unitOfWork.FoundationType.GetName(customQuotationDetail.ConstructDetail.FoundationId);
			customQuotationDetailVM.ConstructDetailVM.Basement = _unitOfWork.BasementType.GetName(customQuotationDetail.ConstructDetail.BasementId);
			customQuotationDetailVM.ConstructDetailVM.Roof = _unitOfWork.RoofType.GetName(customQuotationDetail.ConstructDetail.RooftopId);
			customQuotationDetailVM.ConstructDetailVM.Width = customQuotationDetail.ConstructDetail.Width;
			customQuotationDetailVM.ConstructDetailVM.Length = customQuotationDetail.ConstructDetail.Length;
			customQuotationDetailVM.ConstructDetailVM.Facade = customQuotationDetail.ConstructDetail.Facade;
			customQuotationDetailVM.ConstructDetailVM.Alley	 = customQuotationDetail.ConstructDetail.Alley;
			customQuotationDetailVM.ConstructDetailVM.Floor = customQuotationDetail.ConstructDetail.Floor;
			customQuotationDetailVM.ConstructDetailVM.Mezzanine = customQuotationDetail.ConstructDetail.Mezzanine;				
			customQuotationDetailVM.ConstructDetailVM.RooftopFloor =customQuotationDetail.ConstructDetail.RooftopFloor;
			customQuotationDetailVM.ConstructDetailVM.Garden = customQuotationDetail.ConstructDetail.Garden;

			//lấy 
			//TODO: test result of custom quotation
			//return Json(new { data = customQuotationDetailVM });
			return View(customQuotationDetailVM);
		}
		public IEnumerable<MaterialDetail> GetMateraiDetail(string quoteId, IUnitOfWork unitOfWork)
		{
			return  unitOfWork.MaterialDetail.GetAll().ToList().Where(x => x.QuotationId == quoteId);

			//return result;
		}

		public IActionResult Test()
		{
			var demo = GetMateraiDetail("CQ001", _unitOfWork);

			return Json(new { data = demo });
		}
		
	}


}
