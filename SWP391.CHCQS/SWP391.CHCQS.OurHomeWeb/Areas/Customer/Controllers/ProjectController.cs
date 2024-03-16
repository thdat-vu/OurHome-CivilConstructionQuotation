using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class ProjectController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProjectController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			List<Project> projectList = _unitOfWork.Project.GetAll(includeProperties: "Customer").OrderByDescending(x => x.Date).ToList();
			return View(projectList);
		}

		public async Task<IActionResult> Detail(string? id)
		{
			Project? project = _unitOfWork.Project.Get(filter: u => u.Id == id, includeProperties: "Customer");
			if (project == null)
			{
				return NotFound();
			}
			else
			{
				return View(project);
			}
		}

		public IActionResult Search(string? keyword)
		{
			List<Project> projectList;
			if (keyword == null)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				var keyTrim = keyword.Trim().ToLower();
				projectList = _unitOfWork.Project.GetAll(includeProperties: "Customer")
					.Where(x =>
						(!string.IsNullOrEmpty(x.Overview) && x.Overview.ToLower().Contains(keyTrim)) ||
						(!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(keyTrim)) ||
						x.Name.ToLower().Contains(keyTrim) ||
						x.Size.ToLower().Contains(keyTrim) ||
						x.Location.ToLower().Contains(keyTrim) ||
						x.Customer.Name.ToLower().Contains(keyTrim))
					.OrderByDescending(x => x.Date).ToList();
			}
			return View("Index", projectList);
		}
	}
}
