using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class QuotationController : Controller
    {
        public IActionResult CreateConstructDetails()
        {
            return View();
        }
    }
}
