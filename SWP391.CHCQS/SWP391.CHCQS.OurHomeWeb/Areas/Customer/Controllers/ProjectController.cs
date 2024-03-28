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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 2; // Số lượng dự án trên mỗi trang
                               // Tính toán offset để lấy dữ liệu từ database cho trang hiện tại
            int offset = (page - 1) * pageSize;

            // Lấy danh sách dự án từ database
            List<Project> projectList = _unitOfWork.Project.GetAll(includeProperties: "Customer")
                .OrderByDescending(x => x.Date)
                .Skip(offset)
                .Take(pageSize)
                .ToList();

            // Tính toán tổng số trang dựa trên tổng số dự án
            int totalProjects = _unitOfWork.Project.GetAll().Count();
            int totalPages = (int)Math.Ceiling((double)totalProjects / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

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

        public IActionResult Search(string? keyword, int page = 1)
        {
            if (keyword == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var keyTrim = keyword.Trim().ToLower();
            int pageSize = 2; // Số lượng dự án trên mỗi trang
                              // Tính toán offset để lấy dữ liệu từ database cho trang hiện tại
            int offset = (page - 1) * pageSize;

            var projectList = _unitOfWork.Project.GetAll(includeProperties: "Customer")
                .Where(x =>
                    (!string.IsNullOrEmpty(x.Overview) && x.Overview.ToLower().Contains(keyTrim)) ||
                    (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(keyTrim)) ||
                    x.Name.ToLower().Contains(keyTrim) ||
                    x.Size.ToLower().Contains(keyTrim) ||
                    x.Location.ToLower().Contains(keyTrim) ||
                    x.Customer.Name.ToLower().Contains(keyTrim)).ToList();

            var displayList = projectList.OrderByDescending(x => x.Date)
                                        .Skip(offset)
                                        .Take(pageSize)
                                        .ToList();

            // Tính toán tổng số trang dựa trên tổng số dự án
            int totalProjects = projectList.Count();
            int totalPages = (int)Math.Ceiling((double)totalProjects / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.LastSearch = keyword;
            return View("Index", displayList);
        }
    }
}
