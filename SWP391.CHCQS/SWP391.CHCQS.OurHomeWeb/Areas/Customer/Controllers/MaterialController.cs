using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MaterialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var materialCategories = _unitOfWork.MaterialCategory.GetAll(includeProperties: "Materials").ToList();
            return View(materialCategories);
        }
    }
}
