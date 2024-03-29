using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using System.Diagnostics;
using System.Security.Claims;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM();
             homeVM.Projects = _unitOfWork.Project.GetAll()
				.Where(x => x.Status == true)
				.OrderByDescending(x => x.Date).Take(6).ToList();
            if (homeVM.Projects != null && homeVM.Projects.Count > 0)
            {
				foreach (var project in homeVM.Projects)
				{
					var firstImages = _unitOfWork.ProjectImage.GetAll(x => x.ProjectId == project.Id).FirstOrDefault();
					if (firstImages != null)
					{
						project.Images = new List<ProjectImage>();
                        project.Images.Add(firstImages);
					}
				}
			}
            homeVM.Combos = _unitOfWork.Combo.GetAll()
                .Where(x => x.Status == true)
                .Take(4).ToList();
             return View(homeVM);
            //return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
