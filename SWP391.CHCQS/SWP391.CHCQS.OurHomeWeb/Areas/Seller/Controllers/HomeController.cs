using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
	[Area("Seller")]
    [Authorize(Roles = SD.Role_Seller)]
    public class HomeController : Controller
	{
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Request");
        }
    }
}
