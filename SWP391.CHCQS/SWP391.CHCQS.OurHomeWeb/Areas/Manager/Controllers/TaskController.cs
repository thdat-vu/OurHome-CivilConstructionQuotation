using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;

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

		#region DECLARE SESSION
		public List<TaskVM> TaskListSession => HttpContext.Session.Get<List<TaskVM>>(SessionConst.COMBO_TASK_LIST_KEY) ?? new List<TaskVM>();
		#endregion

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

		public async Task<IActionResult> GetTaskListSession()
		{
			return Json(new { data = TaskListSession.ToList() });
		}

		public async Task<IActionResult> AddToList(string TaskId)
		{
			//Assign TaskListSession to taskCart
			var taskCart = TaskListSession;
			//Get TaskVM and asign to materialItem from taskCart
			var taskItem = taskCart.FirstOrDefault(x => x.Task.Id == TaskId);

			//If taskItem equal null mean the taskItem not in Session (not in session)
			if (taskItem == null)
			{
				//Get a task from database by TaskId
				var task = _unitOfWork.Task.Get(x => x.Id == TaskId);

				//Check if that task not in database
				if (task == null)
				{

					return Json(new { success = false, message = $"Không tìm thấy đầu việc! Mã = {TaskId}" });
				}
				else //if it not equal null
				{
					//Asign new TaskVM with projection from task for taskItem
					taskItem = new TaskVM
					{
						Task = task,
						CategoryName = _unitOfWork.TaskCategory.GetName(task.CategoryId)
					};

					//Add taskItem into taskCart
					taskCart.Add(taskItem);
				}
			}
			else // if it already in session
			{

				return Json(new { success = false, message = $"Đầu việc đã tồn tại! Mã = {TaskId}" });
			}

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.COMBO_TASK_LIST_KEY, taskCart);

			return Json(new { success = true, message = $"Thêm đầu việc thành công! Mã = {TaskId}" });
		}


		[HttpDelete]
		public async Task<IActionResult> DeleteFromList(string TaskId)
		{
			//Asign MaterialListSession to materialCart
			var taskCart = TaskListSession;

			//Get materialItem which exist in materialCart
			var taskItem = taskCart.Where(x => x.Task.Id == TaskId).FirstOrDefault();

			//if materialItem not in materialCart
			if (taskItem == null)
			{
				

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Không tìm thấy đầu việc! Mã = {taskItem}" });
			}

			//Delete materialItem in materialCart
			taskCart.Remove(taskItem);

			//Update MaterialListSession with materialCart  
			HttpContext.Session.Set(SessionConst.COMBO_TASK_LIST_KEY, taskCart);

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = false, message = $"Xóa vật tư thành công! Mã = {taskItem}" });
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
			TempData["success"] = "Thêm đầu mục công việc thành công";
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
			TempData["success"] = "Chỉnh sửa đầu mục công việc thành công";
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
			TempData["success"] = "Xóa đầu mục công việc thành công";
			return RedirectToAction("Index"); //redirect to Index.cshtml
		}
	}
}
