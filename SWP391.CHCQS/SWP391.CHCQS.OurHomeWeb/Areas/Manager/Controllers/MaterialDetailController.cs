using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Linq;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MaterialDetailController : BaseController
    {


        public MaterialDetailController(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : base(unitOfWork, environment)
        {

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
            //lấy note từ session
            var rejectDetail = GetRejectQuotationDetailFromSessionAndFile();

            var materialNote = rejectDetail.MaterialDetailNotes;
            //thêm thông tin material detail
            List<MaterialDetailListViewModel> materialDetailVM = _unitOfWork.MaterialDetail.GetMaterialDetail(quoteId, "Material")
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
                    Note = new KeyValuePair<string, MaterialNote>(x.MaterialId, new MaterialNote()
                    {
                        //Quantity = materialNote[x.MaterialId].Quantity,
                        Note = materialNote[x.MaterialId].Note,
                    })
                }).ToList();


            return Json(new { data = materialDetailVM });
        }

    }
}
