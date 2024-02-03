using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
