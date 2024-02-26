using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class QuotationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuotationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult CreateConstructDetails()
        {
            return View();
        }

        public IActionResult InsertConstructDetails(int? id)
        {
            ConstructDetailViewModel constructDetailVM = new()
            {
                Basement = _unitOfWork.BasementType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Construction = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Foundation = _unitOfWork.FoundationType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Investment = _unitOfWork.InvestmentType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Rooftop = _unitOfWork.RoofType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                ConstructDetail = new ConstructDetail()
            };
            return View(constructDetailVM);
        }


        [HttpPost]
        public IActionResult InsertConstructDetails(ConstructDetailViewModel constructDetailVM) 
        {
                _unitOfWork.Save();
                TempData["success"] = "Construct Detail created successfully";
                return RedirectToAction("Index", "Home");
        }
    }
}
