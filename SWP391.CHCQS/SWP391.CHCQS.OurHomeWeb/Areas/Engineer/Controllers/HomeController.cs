using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Quotation");
        }
    }
}
