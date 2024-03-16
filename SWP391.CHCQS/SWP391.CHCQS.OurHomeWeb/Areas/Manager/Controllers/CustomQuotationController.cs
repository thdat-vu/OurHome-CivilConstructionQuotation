using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Services;
using SWP391.CHCQS.Services.NotificationHub;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Globalization;
using System.Security.Claims;
using EmailSender = SWP391.CHCQS.Utility.Helpers.EmailSender;


namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
	[Area("Manager")]
	[Authorize(Roles = "Manager")]
	public class CustomQuotationController : BaseController
	{
		//private readonly IUnitOfWork _unitOfWork;
		//private readonly UserManager<IdentityUser> _userManager;
		//private readonly IWebHostEnvironment _environment;
		private readonly IConfiguration _configuration;
		private readonly IHubContext<NotificationHub> _hubContext;
		public CustomQuotationController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IConfiguration configuration, UserManager<IdentityUser> userManager, IHubContext<NotificationHub> hubContext) : base(unitOfWork, environment, userManager)
		{
			_configuration = configuration;
			//_userManager = userManager;
			_hubContext = hubContext;
		}
		/// <summary>
		/// Action dẫn đến trang chính để coi quotation
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}
		/// <summary>
		///  Action dẫn đến trang chính để history - reject và completed quotation
		/// </summary>
		/// <returns></returns>
		public IActionResult History()
		{
			return View();
		}
		/// <summary>
		/// hàm lấy ra ClamsIdentity - full thông tin không che của User
		/// </summary>
		/// <returns></returns>
		[NonAction]
		private ClaimsIdentity GetCurrentIdentity()
		{
			return (ClaimsIdentity)User.Identity;
		}
		/// <summary>
		/// hàm lấy ra id của Manager dang đăng nhập vào
		/// </summary>
		/// <returns></returns>
		[NonAction]
		private string GetCurrentUserId()
		{
			return GetCurrentIdentity().FindFirst(ClaimTypes.NameIdentifier).Value;
		}

		#region             GỌI API
		/// <summary>
		/// Action trả về JSON các quotation cần dc xử lý bởi staff đang login, dc hiển thị qua datatable
		/// </summary>
		/// <param name="filterStatus"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] int? filterStatus = null)
		{
			var userId = GetCurrentUserId();
			//lấy ra danh sách requestfrom mà manager đó đảm nhiệm duyệt
			List<string> requestIdList = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.StaffId == userId)
				.Select(x => x.RequestId)
				.Distinct()
				.ToList();

			requestIdList = _unitOfWork.CustomQuotation.GetAllWithFilter(x => requestIdList.Any(r => r == x.RequestId)).Select(x => x.RequestId).ToList();

			List<WorkingReport> workingReports = null;
			//khai báo list giữ các customquotationVm
			List<CustomQuotationListViewModel> customQuotationViewModels = new List<CustomQuotationListViewModel>();
			//lấy ra id của staff tham gia vào requestid
			foreach (var id in requestIdList)
			{
				string slName = null;
				string enName = null;
				string mgName = null;

				var workingReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == id);
				foreach (var workReport in workingReport)
				{
					//lấy nhân viên ra
					var staff = _userManager.FindByIdAsync(workReport.StaffId).GetAwaiter().GetResult() as ApplicationUser;
					//xác nhận role của nhân viên đó
					var role = await _userManager.GetRolesAsync(staff);
					//gán cho biến name với staff role tương ứng
					if (role.First() == SD.Role_Seller)
						slName = staff.Name;
					if (role.First() == SD.Role_Engineer)
						enName = staff.Name;
					if (role.First() == SD.Role_Manager)
						mgName = staff.Name;
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
			//lấy pending quote
			if (filterStatus == SD.Pending_Approval)
				//Thực hiện filter chỉ lấy ra những customquotation dag pending approve
				customQuotationViewModels = customQuotationViewModels.Where(x => x.Status == SD.GetQuotationStatusDescription(SD.Pending_Approval)).OrderByDescending(x => x.Id).ToList();
			//lấy history quote - bao gồm rejected và completed
			if (filterStatus == null) //do lấy 2 trạng thái nên để null
				customQuotationViewModels = customQuotationViewModels
					.Where(x => x.Status == SD.GetQuotationStatusDescription(SD.Rejected) || x.Status == SD.GetQuotationStatusDescription(SD.Completed)).OrderByDescending(x => x.Id).ToList();

			return Json(new { data = customQuotationViewModels });
		}

		/// <summary>
		/// Action trả về JSON các quotation cần dc xử lý bởi staff đang login, dc hiển thị qua datatable, được sử dụng ở datatable ở dashboard View
		/// </summary>
		/// <param></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAllQuote()
		{
			var userId = GetCurrentUserId();
			//lấy ra danh sách id của customquotation
			List<string> cqsId = _unitOfWork.CustomQuotation.GetAll()
				.Select(x => x.Id)
				.ToList();

			//khai báo list giữ các customquotationVm
			List<CustomQuotationListViewModel> customQuotationViewModels = new List<CustomQuotationListViewModel>();
			//lấy ra id của staff tham gia vào requestid
			foreach (var id in cqsId)
			{
				string slName = null;
				string enName = null;
				string mgName = null;
				var requestId = SD.requestIdKey + id.Substring(2);
				var workingReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == requestId);
				foreach (var workReport in workingReport)
				{
					//lấy nhân viên ra
					var staff = _userManager.FindByIdAsync(workReport.StaffId).GetAwaiter().GetResult() as ApplicationUser;
					//xác nhận role của nhân viên đó
					var role = await _userManager.GetRolesAsync(staff);
					//gán cho biến name với staff role tương ứng
					if (role.First() == SD.Role_Seller)
						slName = staff.Name;
					if (role.First() == SD.Role_Engineer)
						enName = staff.Name;
					if (role.First() == SD.Role_Manager)
						mgName = staff.Name;
				}
				//thực hiện tạo 1 đối tượng customQuotation tương ứng
				var cq = _unitOfWork.CustomQuotation.Get(x => x.Id == id, "ConstructDetail,Request");
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
			//thực hiện đảo danh sách từ mới đến cũ
			customQuotationViewModels = customQuotationViewModels.OrderByDescending(x => x.Id).ToList();
			return Json(new { data = customQuotationViewModels });
		}
		#region    API để thực hiện TakeNote 
		/// <summary>
		/// Action để gọi API thực hiện take note cho task, có tác dụng theo từng dòng note, và những takenonte mới này sẽ được lưu vào session
		/// Chỉ có tác dụng lưu trữ khi bấm "Save Note" để gọi tới Action SavNote()
		/// </summary>
		/// <param name="materialId"></param>
		/// <param name="quantity"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult TakeNoteMaterialToSession(string materialId, int quantity, string note)
		{
			string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
			bool isSuccess = true;
			RejectQuotationDetail rejectDetail = null;
			try
			{
				//lấy rejectDetail có trong Session, key tương ứng với quotationId đang xử lý
				rejectDetail = GetRejectQuotationDetailFromSessionAndFile();
				//Tiến hành lưu trữ lại note theo materialId đưa xuống
				//lưu trữ quantity
				rejectDetail.MaterialDetailNotes[materialId].Quantity = quantity;
				//lưu trữ lại note
				rejectDetail.MaterialDetailNotes[materialId].Note = note;
				//gán lại rejectDetail vừa dc cập nhật note
				HttpContext.Session.Set<RejectQuotationDetail>(quoteId, rejectDetail);
			}
			catch (Exception)
			{
				return Json(new { isSuccess = !isSuccess });
			}
			return Json(new { isSuccess, add = rejectDetail });
		}

		/// <summary>
		/// Action để gọi API để thực hiện take note material, có tác dụng theo từng dòng material, và những takenote mới này sẽ được lưu vào session
		/// Chỉ có tác dụng lưu trữ khi bấm "Save Note" để gọi tới Action SavNote()
		/// </summary>
		/// <param name="taskId"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult TakeNoteTaskToSession(string taskId, string note)
		{
			string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
			var isSuccess = true;
			RejectQuotationDetail rejectDetail = null;
			try
			{
				//lấy rejectDetail có trong Session, key tương ứng với quotationId đang xử lý
				rejectDetail = GetRejectQuotationDetailFromSessionAndFile();
				//Tiến hành lưu trữ lại note theo taskId đưa xuống
				rejectDetail.TaskDetailNotes[taskId] = note;

				//gán lại rejectDetail vừa dc cập nhật note
				HttpContext.Session.Set<RejectQuotationDetail>(quoteId, rejectDetail);
			}
			catch (Exception)
			{
				return Json(new { success = !isSuccess });
			}//trả về để thử thôi
			return Json(new { isSuccess, add = rejectDetail });
		}

		/// <summary>
		/// Action để hoàn toàn lưu note xuống file txt để ở note-reject-quotation-file
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult SaveNote()
		{
			string message = "Lưu ghi chú thành công";
			var isSuccess = true;
			try
			{
				//lấy quoteId trong session ra
				string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
				//lấy note từ trong session ra
				var data = HttpContext.Session.Get<RejectQuotationDetail>(quoteId);
				//nếu data null thì ko lưu
				if (data != null)
				{
					var pathCreater = new PathCreater(_environment);
					string targetFolder = pathCreater.CreateFilePathInRoot(quoteId.Trim() + ".txt", "note-reject-quotation-file");
					//Tiến hành lưu trữ
					FileManipulater<RejectQuotationDetail>.SaveJsonToFile(targetFolder, data);
					//khi đã bấm save thì xóa luôn dữ liệu trong session
					HttpContext.Session.Remove(quoteId);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				message = "Có lỗi xảy ra. Lưu ghi chú thất bại";
				return Json(new { isSuccess = !isSuccess, message });
			}
			return Json(new { isSuccess, message });
		}
        #endregion

        #endregion
        

        #region             ACTION 

        /// <summary>
        /// Action trả trang coi chi tiết của customQuotation mà Manager cần xem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
				SubmissionDateSeller = null,
				RecieveDateManager = null,
				//ko cần cập nhật accept của manager khi view Detail
				AcceptanceDateManager = null,
				RecieveDateEngineer = null,
				SubmissionDateEngineer = null,
				Total = cqDetail.Total,
				Status = cqDetail.Status,
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
					engineer = staff as ApplicationUser;
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
		#region     Actions Xử lý Decision cho Manager 
		/// <summary>
		/// Action cung cấp chức năng rejected cho Manager
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> RejectDetail(CustomQuotationVM model)
		{
			#region    Lưu dữ liệu vào bảng RejectionReports
			//lấy id của quotation bị reject đưa cho biến rejectReportId giữ
			var rejectQuotationId = model.RejectReportVM.RejectQuotationId;

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
				SubmitDay = model.RejectReportVM.SubmissionEngineerDate,
				//id engineer đã làm report đó
				EngineerId = model.RejectReportVM.EngineerId,
				//thời gian recieve quotation bị reject của Manager từ WorkingReports table
				ReceiveDay = model.RejectReportVM.RecieveManagerDate,
				//id của manager đã reject report
				ManagerId = model.RejectReportVM.ManagerId,
				//lý do reject tổng quát
				Reason = model.RejectReportVM.Reason
			};
			//Thêm vào RejectionReports table 
			_unitOfWork.RejectedCustomQuotation.Add(rejectReport);
			#endregion

			#region    Cập nhật lại trạng thái dữ liệu cho CustomQuotations
			//lấy custom quotation bị reject ra từ CustomQquotations table
			var rejectCustomQuotation = _unitOfWork.CustomQuotation.Get((x) => x.Id == rejectQuotationId);

			//Thực hiện thay đổi trạng thái của customquotation trong CustomQuotations table thành rejected
			rejectCustomQuotation.Status = SD.Rejected;
			//Lưu xuống database
			_unitOfWork.CustomQuotation.Update(rejectCustomQuotation);
			#endregion

			#region     Reset lại working report cho Engineer và Manager

			//lấy ra workkingReport và thực hiện reset lại thời gian
			var engineerWorkReport = _unitOfWork.WorkingReport.Get(x => x.StaffId == model.RejectReportVM.EngineerId && x.RequestId == rejectCustomQuotation.RequestId);
			//Xóa thời gian submission của Enginner trong WorkingReports table
			engineerWorkReport.SubmitDate = null;

			var managerWorkReport = _unitOfWork.WorkingReport.Get(x => x.StaffId == model.RejectReportVM.ManagerId && x.RequestId == rejectCustomQuotation.RequestId);
			//Xóa thời gian Receive của Manager trong WorkingReports table
			managerWorkReport.ReceiveDate = null;

			//Update WorkingReport cho Engineer và Manager
			_unitOfWork.WorkingReport.Update(engineerWorkReport);
			_unitOfWork.WorkingReport.Update(managerWorkReport);
			#endregion
			//LƯU LẠI
			_unitOfWork.Save();

			//Toast Info lên là reject thành công
			NotifySuccess("Từ chối thành công");

			await _hubContext.Clients.All.SendAsync("RecieveRejectFromManager", "Manager", "Bạn đã nhận 1 báo giá mới");
			//điều hướng người dùng lại trang index để coi List
			return RedirectToAction("Index");
		}
		/// <summary>
		/// Action cung cấp chức năng approve detail cho manager
		/// </summary>
		/// <param name="id"></param>
		/// <param name="managerId"></param>
		/// <returns></returns>
		public async Task<IActionResult> ApproveDetailAsync(string id, string managerId)
		{
			var quotation = _unitOfWork.CustomQuotation.Get((x) => x.Id == id, "Request");

			//thay đổi status cho custom quotation
			quotation.Status = SD.Completed;

			//cập nhật thời gian submit của manager vào WorkingReport table
			var workingManagerReport = _unitOfWork.WorkingReport.Get((x) => x.StaffId == managerId && x.RequestId == quotation.RequestId);

			workingManagerReport.SubmitDate = DateTime.Now;
			//tiến hành cập nhật xuống WorkingReports table
			_unitOfWork.WorkingReport.Update(workingManagerReport);

			//tiến hành cập nhật xuống CustomQuotations table
			_unitOfWork.CustomQuotation.Update(quotation);

			_unitOfWork.Save();

			/*
             * Gửi mail cho người dùng
             - Lấy thông tin người dùng
             - Lấy thông tin dữ liệu để gửi mail người dùng
             */
			//tiến hành lấy thông tin cơ bản của khách hàng
			var customerId = quotation.Request.CustomerId;
			var customer = await _userManager.FindByIdAsync(customerId) as ApplicationUser;
			var customerName = customer.Name;
			var customerMail = customer.Email;
			//tiến hành gửi mail
			var emailSender = new EmailSender(_configuration, _environment);
			if (!emailSender.SendInfoToEmail(customerMail, customerName, id))
			{
				//Tiến hành toast info
				NotifyError("Có lỗi xảy ra. Gửi mail thất bại");
			}
			else
				NotifySuccess("Báo giá đã được gửi");

			//điều hướng người dùng về danh sách pending approve
			return RedirectToAction("Index");
		}
		#endregion
		#endregion


		#region     Actions đưa người dùng tới coi CustomQuotation đã tối giản cho giống PDF, có thể tải pdf xuống
		//Action được tạo ra để render ra 1 html template HỖ TRỢ cho việc tạo ra pdf - được sử dụng để attach theo email báo giá
		[ActionName("Review")]
		//[Authorize(Roles = "Customer")]
		public async Task<IActionResult> ReviewQuotationPDF(string quoteId)
		{
			//Tiến hành lấy quotation đầy đủ ra
			var info = _unitOfWork.CustomQuotation.Get((x) => x.Id == quoteId, "Request,ConstructDetail");

			//Tiến hành fill thông tin cho PDFQuotation
			var pdf = new PDFQuotation
			{
				Id = info.Id,
				Date = info.Date,
				Acreage = info.Acreage,
				Location = info.Location,
				Description = info.Description,
				Total = info.Total,
				GenerateDateRequest = info.Request.GenerateDate,
				ConstructDetail = info.ConstructDetail
			};

			//bổ sung construct Detail
			pdf.ConstructDetail.Basement = _unitOfWork.BasementType.Get((x) => x.Id == info.ConstructDetail.BasementId);

			pdf.ConstructDetail.Foundation = _unitOfWork.FoundationType.Get((x) => x.Id == info.ConstructDetail.FoundationId);

			pdf.ConstructDetail.Construction = _unitOfWork.ConstructionType.Get((x) => x.Id == info.ConstructDetail.ConstructionId);

			pdf.ConstructDetail.Investment = _unitOfWork.InvestmentType.Get((x) => x.Id == info.ConstructDetail.InvestmentId);

			pdf.ConstructDetail.Rooftop = _unitOfWork.RoofType.Get((x) => x.Id == info.ConstructDetail.RooftopId);

			var workingReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == info.RequestId);
			foreach (var workReport in workingReport)
			{
				//lấy nhân viên ra
				var staff = await _userManager.FindByIdAsync(info.Request.CustomerId) as ApplicationUser;
				//xác nhận role của nhân viên đó
				var role = await _userManager.GetRolesAsync(staff);
				//gán cho biến name với staff role tương ứng
				if (role.First() == SD.Role_Seller)
					pdf.SellerName = staff.Name;
				if (role.First() == SD.Role_Engineer)
					pdf.EngineerName = staff.Name;
				if (role.First() == SD.Role_Manager)
					pdf.ManagerName = staff.Name;
			}
			//Lỗi đây nè ~ 
			//pdf.CustomerName = _unitOfWork.Customer.Get((x) => x.Id == info.Request.CustomerId).Name;
			//sửa lại lấy dc tên khách hàng ra
			pdf.CustomerName = (_userManager.FindByIdAsync(info.Request.CustomerId).GetAwaiter().GetResult() as ApplicationUser).Name;
			//tiên hành lấy taskdetail và materialdetail
			pdf.Tasks = new List<TaskDetail>(_unitOfWork.TaskDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Task"));
			pdf.Materials = new List<MaterialDetail>(_unitOfWork.MaterialDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Material"));

			return View(pdf);
		}
		#endregion


		#region     Test Place
		public IActionResult Test()
		{
			var pathCreater = new PathCreater(_environment);
			string targetFolder = pathCreater.CreateFilePathInRoot("CQ003".Trim() + ".txt", "reject-quotation-file");
			var test = FileManipulater<RejectQuotationDetail>.LoadJsonFromFile(targetFolder);
			return Json(new { data = test });
		}
		public IActionResult Test2()
		{
			var test = AppState.Instance(_userManager).GetDelegationIndex();
			return Json(new { data = test });
		}
		#endregion

	}
}



