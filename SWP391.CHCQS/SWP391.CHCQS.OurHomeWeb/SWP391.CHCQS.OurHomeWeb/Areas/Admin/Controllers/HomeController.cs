using Microsoft.AspNetCore.Mvc;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
