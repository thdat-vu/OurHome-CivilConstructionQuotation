using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComboController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComboController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var comboList = _unitOfWork.Combo.GetAll(includeProperties: "Materials,Tasks").ToList();
            //TODO: lấy đc combo + MaterialList TaskList mỗi combo.
            return View(comboList);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var combo = _unitOfWork.Combo.Get(x => x.Id == id);
            return View(combo);
        }
    }
}
