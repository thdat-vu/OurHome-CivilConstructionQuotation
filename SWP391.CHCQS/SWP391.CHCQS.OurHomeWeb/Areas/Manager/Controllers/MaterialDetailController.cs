using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Linq;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MaterialDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        //hàm trả về material detail theo quotation mà Manager đang coi
        //cụ thể là hàm lấy quoteId đã được lưu vào session - hành động này thực hiện ở GetDetail của CustomQuotationController
        //MaterialDetailDTB sẽ gọi ajax đến đây để lấy dữ liệu 
        [HttpGet]
        public IActionResult GetDetail()
        {

            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            //lấy note trong session, để load lại trang có note - nếu ko có thì tạo mới
            var rejectDetail = (HttpContext.Session.Get<RejectQuotationDetail>(quoteId));
            //ko có thì tạo đối tượng rỗng với count = 0
            if (rejectDetail == null)
            {
                rejectDetail = new RejectQuotationDetail()
                {
                    MaterialDetailNotes = new Dictionary<string, MaterialNote>(),
                    TaskDetailNotes = new Dictionary<string, string>()
                };
            }
            List<MaterialDetailListViewModel> materialDetailVM = null;
            //lưu vào Session lại
           // HttpContext.Session.Set(quoteId, rejectDetail);
            var materialNote = rejectDetail.MaterialDetailNotes;
            //thêm thông tin material detail
            materialDetailVM = _unitOfWork.MaterialDetail.GetMaterialDetail(quoteId, "Material")
                .Select((x) => new ViewModels.MaterialDetailListViewModel
                {
                    QuoteId = x.QuotationId,
                    MaterialId = x.MaterialId,
                    //MaterialName = _unitOfWork.Material.GetName(x.MaterialId),
                    MaterialName = x.Material.Name,
                    Unit = x.Material.Unit,
                    MaterialCateName = _unitOfWork.MaterialCategory.GetName(x.Material.CategoryId),
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Note = new KeyValuePair<string, MaterialNote>(x.MaterialId, materialNote.ContainsKey(x.MaterialId) ? materialNote[x.MaterialId] : new MaterialNote()
                    {
                        Quantity = 0,
                        Note = ""
                    })
                }).ToList();


            return Json(new { data = materialDetailVM });
        }

    }
}
