using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

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
        [HttpGet]
        public IActionResult GetDetail()
        {
            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString("quoteId");
            //thêm thông tin material detail
            var materialDetailVM = _unitOfWork.MaterialDetail.GetMaterialDetail(quoteId, "Material")
                .Select((x) => new ViewModels.MaterialDetailViewModel
                {
                    QuoteId = x.QuotationId,
                    MaterialId = x.MaterialId,
                    //MaterialName = _unitOfWork.Material.GetName(x.MaterialId),
                    MaterialName = x.Material.Name,
                    Unit = x.Material.Unit,
                    MaterialCateName = _unitOfWork.MaterialCategory.GetName(x.Material.CategoryId),
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList();
            return Json(new { data = materialDetailVM });
        }
       
    }
}
