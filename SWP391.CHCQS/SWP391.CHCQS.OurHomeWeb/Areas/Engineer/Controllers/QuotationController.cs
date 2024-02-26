using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class QuotationController : Controller
	{
		//Declare _uniteOfWork represent to DBContext to get Data form Database.
		private readonly IUnitOfWork _unitOfWork;

		//Declare Session to store CustomQuotation serve to method AddToList in TaskController and MaterialController to add Task and Material.
		public CustomQuotationListViewModel CustomQuotationSession => HttpContext.Session.Get<CustomQuotationListViewModel>(SessionConst.CUSTOM_QUOTATION_KEY) ?? new CustomQuotationListViewModel();

		//Declare session for CustomQuotationTaskViewModel to store TaskList of the quote when add into quote. if it empty, create one
		public List<CustomQuotationTaskViewModel> TaskListSession => HttpContext.Session.Get<List<CustomQuotationTaskViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<CustomQuotationTaskViewModel>();

		//Khai bao Session cho MaterialList neu co thi lay ra khong co thi tao moi
		public List<MaterialDetailViewModel> MaterialListSession => HttpContext.Session.Get<List<MaterialDetailViewModel>>(SessionConst.MATERIAL_LIST_KEY) ?? new List<MaterialDetailViewModel>();

		//Constructor of this Controller
		public QuotationController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		#region API CALL Custom Quotation List
		/// <summary>
		/// This function get all CustomeQuotation in Database and return it into JSON, this function ne lib Datatables to show data
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Processing)
				.OrderBy(x => x.Date)
				.Select(x => new CustomQuotationListViewModel
				{
					Id = x.Id,
					Date = x.Date,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = SD.GetQuotationStatusDescription(x.Status),
				})
				.ToList();

			return Json(new { data = customQuotationVMList });
		}

		[HttpGet]
		public async Task<IActionResult> GetHistory()
		{
			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Pending_Approval)
				.OrderBy(x => x.Date)
				.Select(x => new CustomQuotationListViewModel
				{
					Id = x.Id,
					Date = x.Date,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = SD.GetQuotationStatusDescription(x.Status),
				})
				.ToList();

			return Json(new { data = customQuotationVMList });
		}

		#endregion


		/// <summary>
		/// This function return the Index of QuotationPage
		/// </summary>
		/// <returns>A view Index</returns>
		public async Task<IActionResult> Index()
		{
			//Remove all session
			HttpContext.Session.Remove(SessionConst.TASK_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.MATERIAL_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.CUSTOM_QUOTATION_KEY);
			return View();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> History()
		{
			//Remove all session
			HttpContext.Session.Remove(SessionConst.TASK_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.MATERIAL_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.CUSTOM_QUOTATION_KEY);
			return View();
		}

		/// <summary>
		/// This function return a form to add Task and Material to CustomQuotation represent CustomQuotationTask and MaterialDetail.
		/// </summary>
		/// <returns>A view create quotation form</returns>
		public async Task<IActionResult> Quote(string QuotationId)
		{
			//Update RecieveDateEngineer 
			var customQuotation = _unitOfWork.CustomQuotation.Get(x => x.Id == QuotationId);
			if (customQuotation.RecieveDateEngineer == null)
			{
				customQuotation.RecieveDateEngineer = DateTime.Now;
				_unitOfWork.CustomQuotation.Update(customQuotation);
				_unitOfWork.Save();

			}

			//Declare constructDetail get data form Database by using _unitOfWork
			ConstructDetail? constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Quotation not found";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Index", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Declare view model to set into Session CustomQuotationSession
			CustomQuotationListViewModel customQuotationViewModel = new CustomQuotationListViewModel();

			//Get only id of customQuotationViewMode from database by using _unitOfWork
			customQuotationViewModel.Id = _unitOfWork.CustomQuotation.Get(x => x.Id == QuotationId).Id;

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null || customQuotationViewModel.Id == null)
			{
				return RedirectToAction("Error", "Home");
			}


			//projection data constructDetail to constructDetailVM
			ConstructDetailViewModel constructDetailVM = new ConstructDetailViewModel
			{
				QuotationId = constructDetail.QuotationId,
				Width = constructDetail.Width,
				Length = constructDetail.Length,
				Facade = constructDetail.Facade,
				Alley = constructDetail.Alley,
				Floor = constructDetail.Floor,
				Room = constructDetail.Room,
				Mezzanine = constructDetail.Mezzanine,
				RooftopFloor = constructDetail.RooftopFloor,
				Balcony = constructDetail.Balcony,
				Garden = constructDetail.Garden,
				ConstructionTypeName = constructDetail.Construction.Name,
				InvestmentTypeName = constructDetail.Investment.Name,
				FoundationTypeName = constructDetail.Foundation.Name,
				RooftopTypeName = constructDetail.Rooftop.Name,
				BasementTypeName = constructDetail.Basement.Name
			};

			//Set customQuotationViewModel after exist in database into CustomQuotationSession
			HttpContext.Session.Set(SessionConst.CUSTOM_QUOTATION_KEY, customQuotationViewModel);

			//Asign TaskListSession for taskCart;
			var taskCart = TaskListSession;

			//if taskCart == null mean the taskCart have no task in there
			if (taskCart.Count == 0)
			{
				taskCart = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id, includeProp: null).Select(x => new CustomQuotationTaskViewModel
				{
					Task = _unitOfWork.Task.Get(t => t.Id == x.TaskId),
					QuotationId = x.QuotationId,
					Price = x.Price,
				}).ToList();
			}

			//Update TaskListSession with taskCart  
			HttpContext.Session.Set(SessionConst.TASK_LIST_KEY, taskCart);

			//Asign MaterialListSession for materialCart;
			var materialCart = MaterialListSession;

			//if materialCart == null mean the taskCart have no task in there
			if (materialCart.Count == 0)
			{
				materialCart = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id, includeProp: null).Select(x => new MaterialDetailViewModel
				{
					Material = _unitOfWork.Material.Get(m => m.Id == x.MaterialId),
					QuotationId = x.QuotationId,
					Quantity = x.Quantity,
					Price = x.Price,
				}).ToList();
			}

			//Update MaterialListSession with materialCart  
			HttpContext.Session.Set(SessionConst.MATERIAL_LIST_KEY, materialCart);

			//return View of this Controller after nothing wrong.
			return View(constructDetailVM);
		}

		/// <summary>
		/// This function will add the TaskList and MaterialList of the Quote into Database
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> SubmitQuote()
		{
			//Asign TaskListSession for taskCart;
			var taskCart = TaskListSession;

			//Asign MaterialListSession for materialCart
			var materialCart = MaterialListSession;

			//if taskCart == null mean the taskCart have no task in there
			if (taskCart.Count == 0)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Task list of quote is empty";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//if materialCart == null mean the materialCart have no material in there
			if (materialCart.Count == 0)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Material list of quote is empty";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//try catch block to catch and resolve error if it occur
			try
			{
				//move item from taskCart(ViewModel) to CustomQuotationTask(Model) to add to database 
				List<CustomQuotationTask> customQuotaionTasks = taskCart.Select(t => new CustomQuotationTask
				{
					TaskId = t.Task.Id,
					QuotationId = t.QuotationId,
					Price = t.Price,
				}).ToList();

				//move item from materialCart(ViewModel) to MaterialDetail(Model) to add to database 
				List<MaterialDetail> materialDetails = materialCart.Select(m => new MaterialDetail
				{
					MaterialId = m.Material.Id,
					QuotationId = m.QuotationId,
					Quantity = m.Quantity,
					Price = m.Price,
				}).ToList();

				//Get data from database to delete
				var customQuotationTasksBeDelete = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id);
				//Detele the old data after get in database
				_unitOfWork.CustomQuotaionTask.RemoveRange(customQuotationTasksBeDelete);
				//Addrange of customQuotaionTasks to database
				_unitOfWork.CustomQuotaionTask.AddRange(customQuotaionTasks);

				//Get data from database to delete
				var materialDetailsBeDelete = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id);
				//Delete the old data after get in database
				_unitOfWork.MaterialDetail.RemoveRange(materialDetailsBeDelete);
				//Addrange of materialDetails to database
				_unitOfWork.MaterialDetail.AddRange(materialDetails);

				var total = materialCart.Sum(x => x.Price) + taskCart.Sum(x => x.Price);

				//update total price of customQuotation after submit
				var customQuotation = _unitOfWork.CustomQuotation.Get(x => x.Id == CustomQuotationSession.Id);
				customQuotation.Total = (decimal)total;
				customQuotation.SubmissionDateEngineer = DateTime.Now;
				_unitOfWork.CustomQuotation.Update(customQuotation);

				//Savechange the database after addrange
				_unitOfWork.Save();

				//Remove all session
				HttpContext.Session.Remove(SessionConst.TASK_LIST_KEY);
				HttpContext.Session.Remove(SessionConst.MATERIAL_LIST_KEY);
				HttpContext.Session.Remove(SessionConst.CUSTOM_QUOTATION_KEY);

				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Success"] = $"Submit quote successfully";

				//Return back to Index of QuotationController
				return View("Index", "Quotation");
			}
			catch (Exception)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Something went wrong";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

		}


		/// <summary>
		/// This function return a form to edit exist quotation
		/// </summary>
		/// <param name="id">Id of the quotation that be selected</param>
		/// <returns>Return a form with detail of the quotation to edit</returns>
		/// onClick=SendQuoteToManager('/Engineer/Quotation/SendToManager?QuotationId=${data}')
		[HttpGet]
		public async Task<IActionResult> SendQuoteToManager(string QuotationId)
		{
			var quotation = _unitOfWork.CustomQuotation.Get(c => c.Id == QuotationId);
			if (quotation == null)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Quotation not found with Id = {QuotationId}" });
			}

			var materialDetails = _unitOfWork.MaterialDetail.GetMaterialDetail(quotation.Id);
			if (materialDetails.Count() == 0)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"This quotation was not complete!" });
			}

			var customQuotationTasks = _unitOfWork.CustomQuotaionTask.GetTaskDetail(quotation.Id);
			if (customQuotationTasks.Count() == 0)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"This quotation was not complete!" });
			}

			try
			{
				quotation.Status = SD.Pending_Approval;
				_unitOfWork.CustomQuotation.Update(quotation);
				_unitOfWork.Save();
			}
			catch (Exception)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Something went wrong" });
			}

			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Send quotation successfully with Id = {QuotationId}" });
		}


		public async Task<IActionResult> Detail(string QuotationId)
		{
			//Declare constructDetail get data form Database by using _unitOfWork
			ConstructDetail? constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
				TempData["Error"] = $"Quotation not found";

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Index", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Declare view model to set into Session CustomQuotationSession
			CustomQuotationListViewModel customQuotationViewModel = new CustomQuotationListViewModel();

			//Get only id of customQuotationViewMode from database by using _unitOfWork
			customQuotationViewModel.Id = _unitOfWork.CustomQuotation.Get(x => x.Id == QuotationId).Id;

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null || customQuotationViewModel.Id == null)
			{
				return RedirectToAction("Error", "Home");
			}


			//projection data constructDetail to constructDetailVM
			ConstructDetailViewModel constructDetailVM = new ConstructDetailViewModel
			{
				QuotationId = constructDetail.QuotationId,
				Width = constructDetail.Width,
				Length = constructDetail.Length,
				Facade = constructDetail.Facade,
				Alley = constructDetail.Alley,
				Floor = constructDetail.Floor,
				Room = constructDetail.Room,
				Mezzanine = constructDetail.Mezzanine,
				RooftopFloor = constructDetail.RooftopFloor,
				Balcony = constructDetail.Balcony,
				Garden = constructDetail.Garden,
				ConstructionTypeName = constructDetail.Construction.Name,
				InvestmentTypeName = constructDetail.Investment.Name,
				FoundationTypeName = constructDetail.Foundation.Name,
				RooftopTypeName = constructDetail.Rooftop.Name,
				BasementTypeName = constructDetail.Basement.Name
			};

			return View(constructDetailVM);
		}

	}
}
