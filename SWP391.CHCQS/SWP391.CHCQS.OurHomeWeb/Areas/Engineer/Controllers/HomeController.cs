using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
    [Area("Engineer")]
    [Authorize(Roles = SD.Role_Engineer)]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Quotation");
        }
    }
}
