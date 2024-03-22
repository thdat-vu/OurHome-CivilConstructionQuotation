using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
	[Authorize(Roles = SD.Role_Manager)]
	public class StaffController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //[ActionName("Detail")]
        //public IActionResult GetDetails(string id)
        //{
        //    var staff = _unitOfWork.Staff.Get((x) => x.Id == id, "Manager");
        //    return Json(new {data = staff});
        //}
        //[HttpGet]
        //[ActionName("Detail")]
        //public IActionResult GetDetail(string staffId)
        //{
        //    var staff = _userManager.FindByIdAsync(staffId).GetAwaiter().GetResult() as ApplicationUser;
        //    return Json(new { data = staff });
        //}
    }
}
