using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
	[Area("Manager")]
	[Authorize(Roles = SD.Role_Manager)]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Dashboard");
		}
	}
}
