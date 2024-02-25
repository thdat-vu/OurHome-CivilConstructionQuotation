using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class TaskController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public TaskController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("Detail")]
        public IActionResult GetDetail([FromQuery] string id)
        {
            var taskDetail = _unitOfWork.Task.Get((x) => x.Id == id, "Category");
            //TODO: Test result
            return Json(new { data = taskDetail });
        }
    }
}
