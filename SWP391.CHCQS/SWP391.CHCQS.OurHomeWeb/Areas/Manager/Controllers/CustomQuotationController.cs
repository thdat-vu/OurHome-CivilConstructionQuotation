using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Services;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Xml.Linq;
using EmailSender = SWP391.CHCQS.Utility.Helpers.EmailSender;


namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
	[Area("Manager")]
	public class CustomQuotationController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IWebHostEnvironment _environment;
		private readonly IConfiguration _configuration;
		
		public CustomQuotationController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IConfiguration configuration, UserManager<IdentityUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_environment = environment;
			_configuration = configuration;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}
		private ClaimsIdentity GetCurrentIdentity()
		{
			return (ClaimsIdentity)User.Identity;

		}
		private string GetCurrentUserId()
		{
			return GetCurrentIdentity().FindFirst(ClaimTypes.NameIdentifier).Value;
		}
		/// This method returns all pending approval CustomQuotation via CustomQuotationListViewModel
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var userId = GetCurrentUserId();

			//lấy ra danh sách requestfrom mà manager đó đảm nhiệm duyệt
			List<string> requestIdList = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.StaffId == userId)
				.Select(x => x.RequestId)
				.Distinct()
				.ToList();
			List<WorkingReport> workingReports = null;

			//khai báo list giữ các customquotationVm
			List<CustomQuotationListViewModel> customQuotationViewModels = new List<CustomQuotationListViewModel>();

			//lấy ra id của staff tham gia vào requestid
			foreach (var id in requestIdList)
			{
				string slName = null;
				string enName = null;
				string mgName = null;

				var wokingReport =  _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == id);
				foreach (var workReport in wokingReport)
				{
					//lấy nhân viên ra
					var staff=  await _userManager.FindByIdAsync(workReport.StaffId);
					//xác nhận role của nhân viên đó
					var role = await _userManager.GetRolesAsync(staff);
					//gán cho biến name với staff role tương ứng
					if (role.First() == SD.Role_Seller)
						slName = staff.UserName;
					if (role.First() == SD.Role_Engineer)
						enName = staff.UserName;
					if (role.First() == SD.Role_Manager)
						mgName = staff.UserName;
				}
				//thực hiện tạo 1 đối tượng customQuotation tương ứng
				var cq = _unitOfWork.CustomQuotation.Get(x => x.RequestId == id, "ConstructDetail,Request");
				//thực hiện chuyển dữ liệu qua customquotationVM
				var cqVM = new CustomQuotationListViewModel()
				{
					Id = cq.Id,
					Date = cq.Date,
					Acreage = cq.Acreage,
					Location = cq.Location,
					Status = SD.GetQuotationStatusDescription(cq.Status),
					SellerName = slName,
					EngineerName = enName,
					ManagerName = mgName,
					ConstrucType = _unitOfWork.ConstructDetail.GetConstructTypeName(cq.ConstructDetail.ConstructionId),
					GeneratRequestDate = cq.Request.GenerateDate
				};
				customQuotationViewModels.Add(cqVM);
			}
			return Json(new { data = customQuotationViewModels });
		}

		[HttpGet]
		public async Task<IActionResult> GetDetail([FromQuery] string id)
		{
			//lưu thông tin quoteId vào session
			HttpContext.Session.SetString(SessionConst.QUOTATION_ID, id);

			//lấy đối tượng customquotation full thông tin từ CustomQuotation table
			var cqDetail = _unitOfWork.CustomQuotation.Get(x => x.Id == id, "ConstructDetail");

			//lấy working report của các staff trong working report
			var workReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == cqDetail.RequestId);

			//tạo đối tượng VM để đưa lên View (đối tượng này còn có 1 vai trò khác đó là RejectDetail sẽ chứa thông tin reject customquotation)
			var quotationVM = new CustomQuotationVM();
			//TIẾN HÀNH CẬP NHẬT THÔNG TIN CHO CustomQuotationDetailViewModel
			quotationVM.QuotationDetailVM = new CustomQuotationDetailViewModel()
			{
				QuoteId = cqDetail.Id,
				RequestId = cqDetail.RequestId,
				//khởi tạo biến constructDetailViewModel để lát nữa giữ dữ liệu
				ConstructDetailVM = new ViewModels.ConstructDetailViewModel(),
				QuoteGeneratedDate = cqDetail.Date,
				//các trường null sẽ dc cập nhật phía dưỡi
				Manager = null,
				Enginneer = null,
				Seller = null,

				DelegationDateSeller = null,
				SubmissionDateSeller =null,

				RecieveDateManager = null,
				//ko cần cập nhật accept của manager khi view Detail
				AcceptanceDateManager = null,

				RecieveDateEngineer = null,
				SubmissionDateEngineer = null,

				Total = cqDetail.Total,
			};
			//thêm thông tin cho construct detail View Model từ CustomQuotation cqDetail
			var constructDetailVM = quotationVM.QuotationDetailVM.ConstructDetailVM;
			constructDetailVM.IsBalcony = cqDetail.ConstructDetail.Balcony;
			constructDetailVM.TypeOfConstruct = _unitOfWork.ConstructionType.GetName(cqDetail.ConstructDetail.ConstructionId);
			constructDetailVM.Investment = _unitOfWork.InvestmentType.GetName(cqDetail.ConstructDetail.InvestmentId);
			constructDetailVM.Foundation = _unitOfWork.FoundationType.GetName(cqDetail.ConstructDetail.FoundationId);
			constructDetailVM.Basement = _unitOfWork.BasementType.GetName(cqDetail.ConstructDetail.BasementId);
			constructDetailVM.Roof = _unitOfWork.RoofType.GetName(cqDetail.ConstructDetail.RooftopId);
			constructDetailVM.Width = cqDetail.ConstructDetail.Width;
			constructDetailVM.Length = cqDetail.ConstructDetail.Length;
			constructDetailVM.Facade = cqDetail.ConstructDetail.Facade;
			constructDetailVM.Alley = cqDetail.ConstructDetail.Alley;
			constructDetailVM.Floor = cqDetail.ConstructDetail.Floor;
			constructDetailVM.Mezzanine = cqDetail.ConstructDetail.Mezzanine;
			constructDetailVM.RooftopFloor = cqDetail.ConstructDetail.RooftopFloor;
			constructDetailVM.Garden = cqDetail.ConstructDetail.Garden;

			//tiến hành lấy id của từng staff sau đó gán Staff cho từng biến lưu trữ tương ứng với mỗi role
			ApplicationUser seller = null;
			ApplicationUser engineer = null;
			ApplicationUser manager = null;
			foreach (var wr in workReport)
			{
				//lấy nhân viên ra
				var staff = await _userManager.FindByIdAsync(wr.StaffId);
				//xác nhận role của nhân viên đó
				var role = await _userManager.GetRolesAsync(staff);
				//gán cho biến name với staff role tương ứng
				if (role.First() == SD.Role_Seller)
				{
					//cập nhật working report của seller tại đây lun 
					quotationVM.QuotationDetailVM.DelegationDateSeller = wr.ReceiveDate;
					quotationVM.QuotationDetailVM.SubmissionDateSeller = wr.SubmitDate;
					seller = staff as ApplicationUser;
				}
				if (role.First() == SD.Role_Engineer)
				{
					//cập nhật working report của engineer tại đây lun 
					quotationVM.QuotationDetailVM.RecieveDateEngineer = wr.ReceiveDate;
					quotationVM.QuotationDetailVM.SubmissionDateEngineer = wr.SubmitDate;
					engineer = staff  as ApplicationUser;
				}
				if (role.First() == SD.Role_Manager)
				{
					//cập nhật thời gian receive quotation của manager nếu đó là lần đầu manager ghé thăm
					if (wr.ReceiveDate == null)
					{
						wr.ReceiveDate = DateTime.Now;
					}
					//cập nhật working report của manager tại đây lun 
					quotationVM.QuotationDetailVM.RecieveDateManager = wr.ReceiveDate;
					manager = staff as ApplicationUser;
				}
			}
			//CẬP NHẬT STAFF TRONG DETAIL
			quotationVM.QuotationDetailVM.Seller = seller;
			quotationVM.QuotationDetailVM.Enginneer = engineer;
			quotationVM.QuotationDetailVM.Manager = manager;

			//TODO: test result of custom quotation
			//return Json(new { data = ID});
			return View(quotationVM);
		}


		//hàm xử lý quyết định của manager - từ chối detail của Engineer trong custom quotation
		[HttpPost]
		public IActionResult RejectDetail(CustomQuotationVM model)
		{
			//lấy id của quotation bị reject đưa cho biến rejectReportId giữ
			var rejectQuotationId = model.RejectReportVM.RejectQuotationId;


			//Working Report của Engineer 
			//var engineerWorkReport = _unitOfWork.WorkingReport.GetBaseOnRequestAndStaffKey(Helper.TransferId(rejectQuotationId, SD.requestIdKey), SD.EngineertIdKey);
			//Working Report của Manager
			//var managerWorkReport = _unitOfWork.WorkingReport.GetBaseOnRequestAndStaffKey(Helper.TransferId(rejectQuotationId, SD.requestIdKey), SD.ManagerIdKey);
			//tạo đối tượng RejectionReport - đối tượng dc sử dụng để lưu xuống RejectionReports table
			var rejectReport = new RejectionReport()
			{
				//TIẾN HÀNH CẬP NHẬP
				Id = SD.TempId,
				//thời gian reject
				RejectedDay = DateTime.Now,
				//id của quotation bị reject
				RejectedQuotationId = rejectQuotationId,
				//thời gian submit quotation bị reject của Enginner từ WorkingReports table
				//SubmitDay = engineerWorkReport.SubmitDate,
				//id engineer đã làm report đó
				//EngineerId = engineerWorkReport.StaffId,
				//thời gian recieve quotation bị reject của Manager từ WorkingReports table
				//ReceiveDay = managerWorkReport.ReceiveDate,
				//id của manager đã reject report
				//ManagerId = managerWorkReport.StaffId,
				//lý do reject tổng quát
				Reason = model.RejectReportVM.Reason
			};
			//Thêm vào RejectionReports table 
			_unitOfWork.RejectedCustomQuotation.Add(rejectReport);

			//lấy custom quotation bị reject ra từ CustomQquotations table
			var rejectCustomQuotation = _unitOfWork.CustomQuotation.Get((x) => x.Id == rejectQuotationId);

			/*
             * 
             Cần thực hiện lưu lại task detail và material detail của custom quotatation
            *
            */
			SaveFile(rejectCustomQuotation.Id, rejectCustomQuotation.Total);


			//Thực hiện thay đổi trạng thái của customquotation trong CustomQuotations table thành rejected
			rejectCustomQuotation.Status = SD.Rejected;

			//Xóa thời gian submission của Enginner trong WorkingReports table
			//engineerWorkReport.SubmitDate = null;
			//Xóa thời gian Receive của Manager trong WorkingReports table
			//managerWorkReport.ReceiveDate = null;

			//Update WorkingReport cho Engineer và Manager
			//_unitOfWork.WorkingReport.Update(engineerWorkReport);
			//_unitOfWork.WorkingReport.Update(managerWorkReport);
			//LƯU LẠI
			_unitOfWork.Save();


			//Toast Info lên là reject thành công
			TempData["Success"] = "Reject Successfull";
			//điều hướng người dùng lại trang index để coi List
			return RedirectToAction("Index");

		}

		[NonAction]
		public IActionResult SaveFile(string rejectQuoteId, decimal? total)
		{
			//tạo đối tượng lưu trữ lại detail của quote bị reject
			RejectQuotationDetail rejectQuotationDetail = new();

			//gán tham số vào Total
			rejectQuotationDetail.Total = total;

			//gán 2 tham số thời gian 
			//rejectQuotationDetail.SubmissionDateEngineer = submit;
			//rejectQuotationDetail.RecieveDateManager = recieve;

			//Lưu trữ lại id của các task
			rejectQuotationDetail.CustomQuotaionTasks = _unitOfWork.TaskDetail.GetAllWithFilter((x) => x.QuotationId == rejectQuoteId).Select((x) => x.TaskId);
			//lưu lại id của các material
			rejectQuotationDetail.MaterialDetails = _unitOfWork.MaterialDetail.GetAllWithFilter((x) => x.QuotationId == rejectQuoteId)
				.ToDictionary(
					(x) => x.MaterialId,
					(x) => x.Quantity
				);

			try
			{
				var pathCreater = new PathCreater(_environment);
				string targetFolder = pathCreater.CreateFilePathInRoot(rejectQuoteId.Trim() + ".txt", "reject-quotation-file");
				//Tiến hành lưu trữ
				FileManipulater<RejectQuotationDetail>.SaveJsonToFile(targetFolder, rejectQuotationDetail);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				TempData["Error"] = "Something happens. Saving Rejected Quotation Detail Fail";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}

		public IActionResult ApproveDetail(string id)
		{
			var quotation = _unitOfWork.CustomQuotation.Get((x) => x.Id == id, "Request");

			//thay đổi status cho custom quotation
			quotation.Status = SD.Completed;

			//cập nhật thời gian submit của manager vào WorkingReport table
			//var report = _unitOfWork.WorkingReport.GetBaseOnRequestAndStaffKey(quotation.RequestId, SD.ManagerIdKey);
			//report.SubmitDate = DateTime.Now;

			//tiến hành cập nhật xuống CustomQuotations table
			_unitOfWork.CustomQuotation.Update(quotation);
			//tiến hành cập nhật xuống WorkingReports table
			//_unitOfWork.WorkingReport.Update(report);

			_unitOfWork.Save();

			/*
             * Gửi mail cho người dùng
             - Lấy thông tin người dùng
             - Lấy thông tin dữ liệu để gửi mail người dùng
             */
			//tiến hành lấy thông tin cơ bản của khách hàng
			var customer = _unitOfWork.Customer.Get((x) => x.Id == quotation.Request.CustomerId);
			var customerName = customer.Name;
			var customerMail = customer.Email;
			//tiến hành gửi mail
			var emailSender = new EmailSender(_configuration, _environment);
			emailSender.SendInfoToEmail(customerMail, customerName, id);

			//Tiến hành toast info
			TempData["Success"] = "Quotation has been sent";
			//điều hướng người dùng về danh sách pending approve
			return RedirectToAction("Index");
		}



		//Action được tạo ra để render ra 1 html template HỖ TRỢ cho việc tạo ra pdf - được sử dụng để attach theo email báo giá
		[ActionName("Review")]
		public IActionResult ReviewQuotationPDF(string quoteId)
		{
			//Tiến hành lấy quotation đầy đủ ra
			var info = _unitOfWork.CustomQuotation.Get((x) => x.Id == quoteId, "Engineer,Manager,Request,Seller,ConstructDetail");

			//Tiến hành fill thông tin cho PDFQuotation
			var pdf = new PDFQuotation();
			pdf.Id = info.Id;
			pdf.Date = info.Date;
			pdf.Acreage = info.Acreage;
			pdf.Location = info.Location;
			pdf.Description = info.Description;
			pdf.Total = info.Total;
			pdf.GenerateDateRequest = info.Request.GenerateDate;
			pdf.ConstructDetail = info.ConstructDetail;

			//bổ sung construct Detail
			pdf.ConstructDetail.Basement = _unitOfWork.BasementType.Get((x) => x.Id == info.ConstructDetail.BasementId);

			pdf.ConstructDetail.Foundation = _unitOfWork.FoundationType.Get((x) => x.Id == info.ConstructDetail.FoundationId);

			pdf.ConstructDetail.Construction = _unitOfWork.ConstructionType.Get((x) => x.Id == info.ConstructDetail.ConstructionId);

			pdf.ConstructDetail.Investment = _unitOfWork.InvestmentType.Get((x) => x.Id == info.ConstructDetail.InvestmentId);

			pdf.ConstructDetail.Rooftop = _unitOfWork.RoofType.Get((x) => x.Id == info.ConstructDetail.RooftopId);

			//pdf.ManagerName = _unitOfWork.WorkingReport.GetStaffNameBaseOnRequestAndStaffKey(info.RequestId, SD.ManagerIdKey);

			//pdf.SellerName = _unitOfWork.WorkingReport.GetStaffNameBaseOnRequestAndStaffKey(info.RequestId, SD.SellerIdKey);

			//pdf.EngineerName = _unitOfWork.WorkingReport.GetStaffNameBaseOnRequestAndStaffKey(info.RequestId, SD.EngineertIdKey);

			pdf.CustomerName = _unitOfWork.Customer.Get((x) => x.Id == info.Request.CustomerId).Name;
			//tiên hành lấy taskdetail và materialdetail
			pdf.Tasks = new List<TaskDetail>(_unitOfWork.TaskDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Task"));
			pdf.Materials = new List<MaterialDetail>(_unitOfWork.MaterialDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Material"));

			return View(pdf);
		}



		public IActionResult Test()
		{
			var test = AppState.Instance(_userManager).GetDelegationIndex();
			return Json(test);
		}




	}


}



