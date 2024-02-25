using Microsoft.AspNetCore.Mvc;
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
            List<Project> projectList = _unitOfWork.Project.GetAll().ToList();
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
    }
}
