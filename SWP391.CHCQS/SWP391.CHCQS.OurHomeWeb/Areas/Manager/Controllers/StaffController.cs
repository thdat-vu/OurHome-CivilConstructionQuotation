using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class StaffController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StaffController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("Detail")]
        public IActionResult GetDetails(string id)
        {
            var staff = _unitOfWork.Staff.Get((x) => x.Id == id, "Manager");
            return Json(new {data = staff});
        }
    }
}
