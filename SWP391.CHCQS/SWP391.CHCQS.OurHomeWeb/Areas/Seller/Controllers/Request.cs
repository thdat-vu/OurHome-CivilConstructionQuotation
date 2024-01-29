using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class Request : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
