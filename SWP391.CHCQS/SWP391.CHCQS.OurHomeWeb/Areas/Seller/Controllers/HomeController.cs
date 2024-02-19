using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
	[Area("Seller")]
	public class HomeController : Controller
	{
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Request");
        }
    }
}
