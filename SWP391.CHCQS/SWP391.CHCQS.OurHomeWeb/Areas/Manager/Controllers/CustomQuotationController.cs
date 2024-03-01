using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class CustomQuotationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        //private FileManipulater _fileManipulater= new FileManipulater();
        public CustomQuotationController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public IActionResult Index()
        {

            return View();
        }

        /// This method returns all pending approval CustomQuotation via CustomQuotationListViewModel
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
        public async Task<IActionResult> GetDetail([FromQuery] string id)
        {

            //mọi thông tin đưa lên sẽ dc đưa vào VM dưới đây giữ 
            var quotationVM = new CustomQuotationVM();
            //lưu thông tin quoteId vào session
            HttpContext.Session.SetString(SessionConst.QUOTATION_ID, id);

            //lấy thông tin cơ bản của custom quotation
            var customQuotationDetail = _unitOfWork.CustomQuotation.Get(x => x.Id == id, "Manager,Engineer,Seller,ConstructDetail");
            //cập nhật thời gian manager nhận dc đơn pending approving
            if (customQuotationDetail.RecieveDateManager == null)
            {
                customQuotationDetail.RecieveDateManager = DateTime.Now;
            }
            //đưa thông tin cho class có vai trò view
            quotationVM.QuotationDetailVM = new CustomQuotationDetailViewModel()
            {
                QuoteId = customQuotationDetail.Id,
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
            quotationVM.QuotationDetailVM.ConstructDetailVM.IsBalcony = customQuotationDetail.ConstructDetail.Balcony;
            quotationVM.QuotationDetailVM.ConstructDetailVM.TypeOfConstruct = _unitOfWork.ConstructionType.GetName(customQuotationDetail.ConstructDetail.ConstructionId);
            quotationVM.QuotationDetailVM.ConstructDetailVM.Investment = _unitOfWork.InvestmentType.GetName(customQuotationDetail.ConstructDetail.InvestmentId);
            quotationVM.QuotationDetailVM.ConstructDetailVM.Foundation = _unitOfWork.FoundationType.GetName(customQuotationDetail.ConstructDetail.FoundationId);
            quotationVM.QuotationDetailVM.ConstructDetailVM.Basement = _unitOfWork.BasementType.GetName(customQuotationDetail.ConstructDetail.BasementId);
            quotationVM.QuotationDetailVM.ConstructDetailVM.Roof = _unitOfWork.RoofType.GetName(customQuotationDetail.ConstructDetail.RooftopId);
            quotationVM.QuotationDetailVM.ConstructDetailVM.Width = customQuotationDetail.ConstructDetail.Width;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Length = customQuotationDetail.ConstructDetail.Length;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Facade = customQuotationDetail.ConstructDetail.Facade;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Alley = customQuotationDetail.ConstructDetail.Alley;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Floor = customQuotationDetail.ConstructDetail.Floor;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Mezzanine = customQuotationDetail.ConstructDetail.Mezzanine;
            quotationVM.QuotationDetailVM.ConstructDetailVM.RooftopFloor = customQuotationDetail.ConstructDetail.RooftopFloor;
            quotationVM.QuotationDetailVM.ConstructDetailVM.Garden = customQuotationDetail.ConstructDetail.Garden;


            //TODO: test result of custom quotation
            //return Json(new { data = ID});
            return View(quotationVM);
        }


        //hàm xử lý quyết định của manager từ chối detail của Engineer trong custom quotation
        [HttpPost]
        public IActionResult RejectDetail(CustomQuotationVM model)
        {
            //đưa dữ liệu cho đối tượng có thể dc thêm vào database
            var rejectQuotationId = model.RejectQuotationDetailVM.RejectQuotationId;
            var rejectCustomQuotationDetail = new RejectedCustomQuotation()
            {
                Id = SD.TempId,
                RejectedQuotationId = rejectQuotationId,
                ManagerId = model.RejectQuotationDetailVM.RejecterId,
                EngineerId = model.RejectQuotationDetailVM.SubcriberId,
                //cập nhật thời gian reject
                Date = DateTime.Now,
                Reason = model.RejectQuotationDetailVM.Reason
            };
            //lưu lại các thông tin cần thiết và bảng rejectcustomquotation
            _unitOfWork.RejectedCustomQuotation.Add(rejectCustomQuotationDetail);

            //lấy target custom quotation bị reject ra trong custom quotation table
            var target = _unitOfWork.CustomQuotation.Get((x) => x.Id == rejectQuotationId);

            /*
             * 
             * 
             * 
             * 
             Cần thực hiện lưu lại task detail và material detail của custom quotatation
            */
            SaveFile(target.Id, target.SubmissionDateEngineer, target.RecieveDateManager, target.Total);


            //Thực hiện thay đổi trạng thái thành rejected
            target.Status = SD.Rejected;

            //thực hiện xóa thời gian đã submit của engineer
            target.SubmissionDateEngineer = null;
            //Thực hiện xóa thời gian nhận của Manager
            target.RecieveDateManager = null;

            //Update lại custom quotation trong custom quotation table
            _unitOfWork.CustomQuotation.Update(target);
            _unitOfWork.Save();

            //điều hướng người dùng lại trang index 
            return RedirectToAction("Index");

        }

        [NonAction]
        public IActionResult SaveFile(string rejectQuoteId, DateTime? submit, DateTime? recieve, decimal? total)
        {
            //tạo đối tượng lưu trữ lại detail của quote bị reject
            RejectQuotationDetail rejectQuotationDetail = new();

            //gán tham số vào Total
            rejectQuotationDetail.Total = total;

            //gán 2 tham số thời gian 
            rejectQuotationDetail.SubmissionDateEngineer = submit;
            rejectQuotationDetail.RecieveDateManager = recieve;

            //Lưu trữ lại id của các task
            rejectQuotationDetail.CustomQuotaionTasks = _unitOfWork.CustomQuotaionTask.GetAllWithFilter((x) => x.QuotationId == rejectQuoteId).Select((x) => x.TaskId);
            //lưu lại id của material dc sử dụng
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
            var quotation = _unitOfWork.CustomQuotation.Get((x) => x.Id == id);
            //cập nhật thời gian approve của Manager
            quotation.AcceptanceDateManager = DateTime.Now;

            //thay đổi status cho custom quotation
            quotation.Status = SD.Completed;


            //tiến hành cập nhật xuống database
            _unitOfWork.CustomQuotation.Update(quotation);
            _unitOfWork.Save();

            //lấy thông tin người dùng

            /*
			 * Gửi mail cho người dùng
			 - Lấy thông tin người dùng
			 - Lấy thông tin dữ liệu để gửi mail người dùng
			 */
            var pathCreater = new PathCreater(_environment);
            string targetFolder = pathCreater.CreateFilePathInRoot( "1.pdf", "pdf");
            //lấy địa chỉ mail
            //bytes is parameter.
            
            EmailSender.SendInfoToEmailAsync("datnxse172264@fpt.edu.vn", "CC","Xinchao", targetFolder);

            //Tiến hành toast info
            TempData["Success"] = "Quotation has been sent";
            //điều hướng người dùng về danh sách pending approve
            return RedirectToAction("Index");
        }

      

        //Action được tạo ra để render ra 1 html template HỖ TRỢ cho việc tạo ra pdf - được sử dụng để attach theo email báo giá
        [ActionName("PDFQuotation")]
        public IActionResult GetDetailQuotationTableHTML(string quoteId)
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
            pdf.ManagerName = info.Manager.Name;
            pdf.SellerName = info.Seller.Name;
            pdf.EngineerName = info.Engineer.Name;
            //tiên hành lấy taskdetail và materialdetail
            pdf.Tasks = new List<CustomQuotationTask>(_unitOfWork.CustomQuotaionTask.GetAllWithFilter((x) => x.QuotationId == info.Id, "Task"));
            pdf.Materials = new List<MaterialDetail>(_unitOfWork.MaterialDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Material"));
            return View(pdf);
        }
        public IActionResult Test()
        {
            var pathCreater = new PathCreater(_environment);
            string targetFolder = pathCreater.CreateFilePathInRoot("1.pdf", "pdf");
            //lấy địa chỉ mail
            //bytes is parameter.

            EmailSender.SendInfoToEmailAsync("datnxse172264@fpt.edu.vn", "CC", "Xinchao", targetFolder);
            return Json(new { notify = "done" });
        }

        public void ExportPDF(PDFQuotation quotation)
        {
            


        }

    }


}
