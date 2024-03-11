using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using Task = SWP391.CHCQS.Model.Task;

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
		//public IActionResult Index()
		//{
		//    return View();
		//}

		#region API CALLS
		/// <summary>
		/// This method return Json of Material List so that Database of Material can read.
		/// </summary>
		/// <returns></returns>
		
		[HttpGet]
        [ActionName("Detail")]
        public async Task<IActionResult> GetDetail([FromQuery] string id)
        {
            var taskDetail = _unitOfWork.Task.Get((x) => x.Id == id, "Category");
            //TODO: Test result
            return Json(new { data = taskDetail });
        }



		[HttpGet]
		public IActionResult GetAll()
		{
			List<Task> objTaskList = _unitOfWork.Task.GetAllWithFilter(filter: m => m.Status == true, includeProperties: "Category").ToList();
			return Json(new { data = objTaskList }); //json + task list for data table.
		}
		#endregion
		
		
		[HttpGet]
        public IActionResult Index()
        {
            List<Task> objTaskList = _unitOfWork.Task.GetAll().ToList();
            
            return View(objTaskList); //redirect to Index.cshtml + objList
        }

        public IActionResult Create()
        {
			//add TaskViewModel to pass Properties.

			TaskVM taskVM = new()
			{
				TaskCategoryList = _unitOfWork.TaskCategory.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Task = new Task()
			};
			return View(taskVM); 
        }

        [HttpPost]
        public IActionResult Create(TaskVM taskVM)
        {
			taskVM.Task.Status = true; //change status into true;
			taskVM.Task.Id = SD.TempId; //assign temp ID


			_unitOfWork.Task.Add(taskVM.Task); //Add Task to task table
			_unitOfWork.Save(); //keep track on change
			TempData["success"] = "Task created successfully";
			return RedirectToAction("Index");
		}

		//Edit Action
		public IActionResult Edit(string? id)
		{
			if (id == null) //id is null
			{
				return NotFound();//return status not found aka 404
			}
			//step 1:retrieve Task from DB
			Task? taskFromDb = _unitOfWork.Task.Get(u => u.Id == id);

			//catch not found exception
			if (taskFromDb == null)
			{
				return NotFound();//return status not found aka 404
			}
			//step 2: pass the Task Properties to TaskVM instance
			//step 2.1: create MaterialViewModel
			TaskVM taskVM = new()
			{
				TaskCategoryList = _unitOfWork.TaskCategory.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Task = new Task()
			};
			//step 2.2: pass Task to TaskViewModel
			taskVM.Task = taskFromDb;
			return View(taskVM); //return View + retrieved Task
		}

		[HttpPost]
		public IActionResult Edit(TaskVM taskVM) 
		{
			_unitOfWork.Task.Update(taskVM.Task); //Update Material to Material table
			_unitOfWork.Save(); //keep track on change
			TempData["success"] = "Task edited successfully";
			return RedirectToAction("Index");
		}

		//Delete Action
		public IActionResult Delete(string? id)
		{
			//catch null id exception
			if (id == null)
			{
				return NotFound(); //return status not found aka 404
			}

			//retrieve Task from DB
			Task? taskFromDb = _unitOfWork.Task.Get(u => u.Id == id, includeProperties: "Category");

			if (taskFromDb == null)
			{
				return NotFound(); //return status not found aka 404
			}

			return View(taskFromDb); //return Delete.cshtml + materialFromDb

		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(string? id)
		{
			//retrieve Task from Db
			Task? obj = _unitOfWork.Task.Get(u => u.Id == id);
			//handle id null exception
			if (obj == null)
			{
				return NotFound();//return status not found aka 404
			}

			//change the status in to false
			obj.Status = false;
			_unitOfWork.Save();//keep track on change
			TempData["success"] = "Task deleted successfully";
			return RedirectToAction("Index"); //redirect to Index.cshtml
		}
	}
}
