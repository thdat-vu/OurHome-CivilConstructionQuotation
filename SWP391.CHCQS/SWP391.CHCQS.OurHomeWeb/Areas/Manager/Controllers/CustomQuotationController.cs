using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;


namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
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

       
        /// This method returns all pending approval CustomQuotation via CustomQuotationViewModel
        [HttpGet]
		public IActionResult GetAll()
		{
			List<CustomQuotationListViewModel> customQuotationViewModels = _unitOfWork.CustomQuotation
				.GetAll(includeProperties: "Engineer,Manager,Seller,ConstructDetail,Request")
				.Where(x => x.Status == SD.Pending_Approval)
				.Select(x => new CustomQuotationListViewModel
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
			return Json(new { data = customQuotationViewModels });
		}
        [HttpGet]
		//tại sao request Get này cần async ???
		//Để có việc lưu quotation Id vào session thành công, sau đó datatable mới lấy quotation Id từ session ra để lấy được dữ liệu task detail và material detail tương ứng
        public async Task<IActionResult> GetDetail([FromQuery] string id)
		{
			//lưu thông tin quoteId vào session
			HttpContext.Session.SetString(SessionConst.QUOTATION_ID, id);

            //lấy thông tin cơ bản của custom quotation
            var customQuotationDetail = _unitOfWork.CustomQuotation.Get(x => x.Id == id, "Manager,Engineer,Seller,ConstructDetail");
			var customQuotationDetailVM = new CustomQuotationDetailViewModel()
			{
				RequestId = customQuotationDetail.RequestId,
				//khởi tạo biến constructDetailViewModel để lát nữa giữ dữ liệu
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
			//thêm thông tin cho construct detail View Model
			customQuotationDetailVM.ConstructDetailVM.IsBalcony = customQuotationDetail.ConstructDetail.Balcony;
			customQuotationDetailVM.ConstructDetailVM.TypeOfConstruct = _unitOfWork.ConstructionType.GetName(customQuotationDetail.ConstructDetail.ConstructionId);
			customQuotationDetailVM.ConstructDetailVM.Investment = _unitOfWork.InvestmentType.GetName(customQuotationDetail.ConstructDetail.InvestmentId);
			customQuotationDetailVM.ConstructDetailVM.Foundation = _unitOfWork.FoundationType.GetName(customQuotationDetail.ConstructDetail.FoundationId);
			customQuotationDetailVM.ConstructDetailVM.Basement = _unitOfWork.BasementType.GetName(customQuotationDetail.ConstructDetail.BasementId);
			customQuotationDetailVM.ConstructDetailVM.Roof = _unitOfWork.RoofType.GetName(customQuotationDetail.ConstructDetail.RooftopId);
			customQuotationDetailVM.ConstructDetailVM.Width = customQuotationDetail.ConstructDetail.Width;
			customQuotationDetailVM.ConstructDetailVM.Length = customQuotationDetail.ConstructDetail.Length;
			customQuotationDetailVM.ConstructDetailVM.Facade = customQuotationDetail.ConstructDetail.Facade;
			customQuotationDetailVM.ConstructDetailVM.Alley = customQuotationDetail.ConstructDetail.Alley;
			customQuotationDetailVM.ConstructDetailVM.Floor = customQuotationDetail.ConstructDetail.Floor;
			customQuotationDetailVM.ConstructDetailVM.Mezzanine = customQuotationDetail.ConstructDetail.Mezzanine;
			customQuotationDetailVM.ConstructDetailVM.RooftopFloor = customQuotationDetail.ConstructDetail.RooftopFloor;
			customQuotationDetailVM.ConstructDetailVM.Garden = customQuotationDetail.ConstructDetail.Garden;

            
			//TODO: test result of custom quotation
			//return Json(new { data = ID});
			return View(customQuotationDetailVM);
		}
        



        //      public IActionResult Test()
        //{
        //	var demo = _unitOfWork.CustomQuotaionTask.GetTaskDetail("CQ001");


        //          return Json(new { data = demo });
        //}

    }


}
