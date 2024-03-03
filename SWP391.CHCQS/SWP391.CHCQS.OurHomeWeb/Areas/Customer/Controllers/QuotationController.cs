using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class QuotationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateRequest()
        {
            RequestForm requestForm = new RequestForm();
            return View(requestForm);  
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
