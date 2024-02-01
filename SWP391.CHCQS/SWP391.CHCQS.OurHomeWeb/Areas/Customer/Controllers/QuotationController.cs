using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QuotationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SendRequest()
        {
            return View();  
        }

        public async Task<IActionResult> ManageRequest()
        {
            return View("RequestHistory");
        }

		public async Task<IActionResult> RequestHistory()
		{
			return View();
		}

		public async Task<IActionResult> ViewResponse()
        {
            return View();
        }
    }
}
