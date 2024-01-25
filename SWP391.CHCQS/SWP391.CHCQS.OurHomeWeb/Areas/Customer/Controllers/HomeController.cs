using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.OurHomeWeb.Models;
using System.Diagnostics;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Combo()
        {
            return View();
        }

        public IActionResult Project()
        {
            return View();
        }

        public IActionResult Material()
        {
            return View();
        }

        public IActionResult Quotation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
