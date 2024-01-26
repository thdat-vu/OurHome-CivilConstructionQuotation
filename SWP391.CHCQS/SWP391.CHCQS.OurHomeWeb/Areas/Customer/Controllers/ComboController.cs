using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComboController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
