using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Services;
using SWP391.CHCQS.Services.NotificationHub;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Security.Claims;
using EmailSender = SWP391.CHCQS.Utility.Helpers.EmailSender;


namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    // [Authorize(Roles = SD.Role_Manager)]
    [Authorize(Roles = "Manager")]



    public class CustomQuotationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;
        public CustomQuotationController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IConfiguration configuration, UserManager<IdentityUser> userManager, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _configuration = configuration;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public IActionResult SaveNote()
        {
            
            var url = Url.Action("/Manager/CustomQuotation/GetDetail", new { filterStatus = 3 });
            return Redirect(url);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult History()
        {
            return View();
        }

        //Hàm trả về danh sách customQuotation cần dc xử lý bởi staff đang login
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
                    var staff =  _userManager.FindByIdAsync(workReport.StaffId).GetAwaiter().GetResult() as ApplicationUser;
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
            if (filterStatus == 3)
                //Thực hiện filter chỉ lấy ra những customquotation dag pending approve
                customQuotationViewModels = customQuotationViewModels.Where(x => x.Status == SD.GetQuotationStatusDescription(SD.Pending_Approval)).OrderByDescending(x => x.Id).ToList();
            //lấy history quote - bao gồm rejected và completed
            if (filterStatus == null)
                customQuotationViewModels = customQuotationViewModels
                    .Where(x => x.Status == SD.GetQuotationStatusDescription(SD.Rejected) || x.Status == SD.GetQuotationStatusDescription(SD.Completed)).ToList();

            return Json(new { data = customQuotationViewModels });
        }

        //Hàm đi tới chi tiết của customQuotation mà Manager cần xem
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

        //hàm xử lý quyết định của manager - từ chối detail của Engineer trong custom quotation
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

            #region     Lưu trữ các take note xuống file, bổ sung thêm các task,material detail dù ko có note

            //Tiến hành lấy RejectQuotationDetaiol từ session
            var rejectQuoteDetail = GetRejectQuotationDetailFromSession();

            SaveNoteToFile(rejectCustomQuotation.Id, rejectQuoteDetail);
            #endregion

            //Khi tất cả đã dc lưu xong xuôi thì xóa sesion chứa note đi
            HttpContext.Session.Set<RejectQuotationDetail>(rejectQuotationId, null);
            //LƯU LẠI
            _unitOfWork.Save();

            //Toast Info lên là reject thành công
            TempData["Success"] = "Reject Successfull";

            await _hubContext.Clients.All.SendAsync("RecieveRejectFromManager", "Manager", "You was recieve a new Quotation");
            //điều hướng người dùng lại trang index để coi List
            return RedirectToAction("Index");
        }

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
                TempData["Error"] = "Something is broken. Send email fail";
            }
            else TempData["Success"] = "Quotation has been sent";

            //điều hướng người dùng về danh sách pending approve
            return RedirectToAction("Index");
        }
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

        #region     Action để thực hiện TakeNote 
        //Đây là non aciton để lưu trự đối tượng RejectQuotationDetail vào trong session với mỗi phiên truy cập vào customquotation
        [HttpPost]
        public IActionResult TakeNoteMaterialToSession(string materialId, int quantity, string note)
        {
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            var isSuccess = true;
            RejectQuotationDetail rejectDetail = null;
            try
            {
                //lấy rejectDetail có trong Session, key tương ứng với quotationId đang xử lý
                rejectDetail = GetRejectQuotationDetailFromSession();
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
                isSuccess = false;
                return Json(new { success = isSuccess });
            }
            return Json(new { success = isSuccess, add = rejectDetail });
        }

        //Đây là non aciton để lưu trự đối tượng RejectQuotationDetail vào trong session với mỗi phiên truy cập vào customquotation
        [HttpPost]
        public IActionResult TakeNoteTaskToSession(string taskId, string note)
        {
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            var isSuccess = true;
            RejectQuotationDetail rejectDetail = null;
            try
            {
                //lấy rejectDetail có trong Session, key tương ứng với quotationId đang xử lý
                rejectDetail = GetRejectQuotationDetailFromSession();
                //Tiến hành lưu trữ lại note theo taskId đưa xuống
                rejectDetail.TaskDetailNotes[taskId] = note;

                //gán lại rejectDetail vừa dc cập nhật note
                HttpContext.Session.Set<RejectQuotationDetail>(quoteId, rejectDetail);
            }
            catch (Exception)
            {
                isSuccess = false;
                return Json(new { success = isSuccess });
            }
            return Json(new { success = isSuccess, add = rejectDetail });
        }
        #endregion

        #region     NonAction để hỗ trợ các Action lớn
        //hàm hỗ trợ cho actions
        [NonAction]
        public IActionResult SaveNoteToFile(string quoteId, RejectQuotationDetail data)
        {
            try
            {
                var pathCreater = new PathCreater(_environment);
                string targetFolder = pathCreater.CreateFilePathInRoot(quoteId.Trim() + ".txt", "reject-quotation-file");
                //Tiến hành lưu trữ
                FileManipulater<RejectQuotationDetail>.SaveJsonToFile(targetFolder, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TempData["Error"] = "Something happens. Saving Rejected Quotation Detail Fail";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [NonAction]
        private ClaimsIdentity GetCurrentIdentity()
        {
            return (ClaimsIdentity)User.Identity;
        }
        [NonAction]
        private string GetCurrentUserId()
        {
            return GetCurrentIdentity().FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        [NonAction]
        private RejectQuotationDetail GetRejectQuotationDetailFromSession()
        {
            var quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            //lấy rejectDetail có trong Session, key tương ứng với quotationId đang xử lý
            var rejectDetail = HttpContext.Session.Get<RejectQuotationDetail>(quoteId);
            //nếu ko có tồn tại rejectDetail trong session thì tạo mới, lưu vào session
            //if (rejectDetail.MaterialDetailNotes.Count() == 0 && rejectDetail.TaskDetailNotes.Count() == 0)
            if(rejectDetail == null) 
            {
                rejectDetail = new RejectQuotationDetail()
                {
                    MaterialDetailNotes = new Dictionary<string, MaterialNote>(),
                    TaskDetailNotes = new Dictionary<string, string>(),
                };
                //Tiến hành tạo các chỗ chứa note cho từng taskId và materialId dựa trên taskDetail và materialDetail
                //lấy ra các taskId thuộc về customquotation đó 
                var taskIdDetailList = _unitOfWork.TaskDetail.GetAllWithFilter((t) => t.QuotationId == quoteId).Select(t => t.TaskId).ToList();
                //đưa default value vào - place holder
                taskIdDetailList.ForEach(t =>
                {
                    rejectDetail.TaskDetailNotes.Add(t, "");
                });
                //lấy ra các materialId thuộc về customquotation đó 
                var materialIdDetailList = _unitOfWork.MaterialDetail.GetAllWithFilter((t) => t.QuotationId == quoteId).Select(t => t.MaterialId).ToList();
                //đưa default value vào - place holder
                materialIdDetailList.ForEach(m =>
                {
                    rejectDetail.MaterialDetailNotes.Add(m, new MaterialNote()
                    {
                        Quantity = 0,
                        Note = ""
                    });
                });
                //lưu trữ với Key tương ứng với Id của customquotation
                HttpContext.Session.Set<RejectQuotationDetail>(quoteId, rejectDetail);
            }
            return rejectDetail;
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



