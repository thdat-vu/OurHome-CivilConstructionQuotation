using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProjectController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
