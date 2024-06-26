﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
<<<<<<< HEAD
=======
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.Services.NotificationHub;
>>>>>>> Demostration
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
<<<<<<< HEAD
	public class QuotationController : Controller
	{
		//Declare _uniteOfWork represent to DBContext to get Data form Database.
		private readonly IUnitOfWork _unitOfWork;

=======
	[Authorize(Roles = SD.Role_Engineer)]
	public class QuotationController : Controller
	{


		#region ============ DECLARE ============


		//Declare _uniteOfWork represent to DBContext to get Data form Database.
		private readonly IUnitOfWork _unitOfWork;

		private readonly IWebHostEnvironment _environment;

		//Declare NotificationHub
		private readonly IHubContext<NotificationHub> _hubContext;

>>>>>>> Demostration
		//Declare Session to store CustomQuotation serve to method AddToList in TaskController and MaterialController to add Task and Material.
		public CustomQuotationListViewModel CustomQuotationSession => HttpContext.Session.Get<CustomQuotationListViewModel>(SessionConst.CUSTOM_QUOTATION_KEY) ?? new CustomQuotationListViewModel();

		//Declare session for CustomQuotationTaskViewModel to store TaskList of the quote when add into quote. if it empty, create one
<<<<<<< HEAD
		public List<CustomQuotationTaskViewModel> TaskListSession => HttpContext.Session.Get<List<CustomQuotationTaskViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<CustomQuotationTaskViewModel>();
=======
		public List<TaskDetailViewModel> TaskListSession => HttpContext.Session.Get<List<TaskDetailViewModel>>(SessionConst.TASK_LIST_KEY) ?? new List<TaskDetailViewModel>();
>>>>>>> Demostration

		//Khai bao Session cho MaterialList neu co thi lay ra khong co thi tao moi
		public List<MaterialDetailViewModel> MaterialListSession => HttpContext.Session.Get<List<MaterialDetailViewModel>>(SessionConst.MATERIAL_LIST_KEY) ?? new List<MaterialDetailViewModel>();

<<<<<<< HEAD
		//Constructor of this Controller
		public QuotationController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		#region API CALL Custom Quotation List
=======

		//Constructor of this Controller
		public QuotationController(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext, IWebHostEnvironment environment)
		{
			_unitOfWork = unitOfWork;
			_hubContext = hubContext;
			_environment = environment;
		}


		#endregion ============ DECLARE ============



		#region ============ API ============


>>>>>>> Demostration
		/// <summary>
		/// This function get all CustomeQuotation in Database and return it into JSON, this function ne lib Datatables to show data
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
<<<<<<< HEAD
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
=======
			List<WorkingReport> workingReports = _unitOfWork.WorkingReport
				.GetAll()
				.Where(x => x.StaffId == GetCurrentUserId())
				.ToList();

			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(cq => cq.Status == SD.Processing)
				.Where(cq => workingReports.Any(wr => wr.RequestId == cq.RequestId))
				.OrderBy(cq => cq.Date)
				.Select(cq => new CustomQuotationListViewModel
				{
					Id = cq.Id,
					Date = cq.Date,
					Acreage = cq.Acreage,
					Location = cq.Location,
					Status = SD.GetQuotationStatusDescription(cq.Status),
					Total = cq.Total,
>>>>>>> Demostration
				})
				.ToList();

			return Json(new { data = customQuotationVMList });
		}

<<<<<<< HEAD
		[HttpGet]
		public async Task<IActionResult> GetHistory()
		{
			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Pending_Approval || x.Status == SD.Completed)
=======

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetHistory()
		{
			List<WorkingReport> workingReports = _unitOfWork.WorkingReport
				.GetAll()
				.Where(x => x.StaffId == GetCurrentUserId())
				.ToList();

			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Pending_Approval || x.Status == SD.Completed || x.Status == SD.Rejected)
				.Where(cq => workingReports.Any(wr => wr.RequestId == cq.RequestId))
>>>>>>> Demostration
				.OrderBy(x => x.Date)
				.Select(x => new CustomQuotationListViewModel
				{
					Id = x.Id,
					Date = x.Date,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = SD.GetQuotationStatusDescription(x.Status),
<<<<<<< HEAD
=======
					Total = x.Total,
>>>>>>> Demostration
				})
				.ToList();

			return Json(new { data = customQuotationVMList });
		}

<<<<<<< HEAD
		[HttpGet]
		public async Task<IActionResult> GetRejected()
		{
			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Rejected)
=======

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetRejected()
		{
			List<WorkingReport> workingReports = _unitOfWork.WorkingReport
				.GetAll()
				.Where(x => x.StaffId == GetCurrentUserId())
				.ToList();

			List<CustomQuotationListViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.Where(x => x.Status == SD.Rejected)
				.Where(cq => workingReports.Any(wr => wr.RequestId == cq.RequestId))
>>>>>>> Demostration
				.OrderBy(x => x.Date)
				.Select(x => new CustomQuotationListViewModel
				{
					Id = x.Id,
					Date = x.Date,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = SD.GetQuotationStatusDescription(x.Status),
<<<<<<< HEAD
=======
					Total = x.Total,
>>>>>>> Demostration
				})
				.ToList();

			return Json(new { data = customQuotationVMList });
		}

<<<<<<< HEAD
		#endregion
=======

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
				return Json(new { success = false, message = $"Không tìm thấy báo giá với mã = {QuotationId}" });
			}

			var materialDetails = _unitOfWork.MaterialDetail.GetMaterialDetail(quotation.Id);
			if (materialDetails.Count() == 0)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Báo giá này chưa hoàn thành!" });
			}

			var customQuotationTasks = _unitOfWork.TaskDetail.GetTaskDetail(quotation.Id);
			if (customQuotationTasks.Count() == 0)
			{
				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return Json(new { success = false, message = $"Báo giá này chưa hoàn thành!" });
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
				return Json(new { success = false, message = $"Có lỗi xảy ra, vui lòng thử lại sau!" });
			}


			//Update SubmitDateEngineer 
			try
			{
				var workingReport = _unitOfWork.WorkingReport
					.Get(wr => wr.StaffId == GetCurrentUserId() && wr.RequestId == quotation.RequestId);
				if (workingReport != null)
					if (workingReport.SubmitDate == null)
						workingReport.SubmitDate = DateTime.Now;
				_unitOfWork.WorkingReport.Update(workingReport);
				_unitOfWork.Save();
			}
			catch (Exception)
			{
				TempData["Error"] = $"Có lỗi xảy ra, vui lòng thử lại sau!";
				return RedirectToAction("Index", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Send notification to Manager
			await _hubContext.Clients.All.SendAsync("RecieveQuotationFromEngineer", "Engineer", "You was recieve a new Quotation");
			//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
			return Json(new { success = true, message = $"Gửi báo giá thành công! Mã báo giá = {QuotationId}" });
		}

		[HttpGet]
		public async Task<IActionResult> GetQuotationBill()
		{
			//Asign TaskListSession for taskCart;
			var taskCart = TaskListSession;

			//Asign MaterialListSession for materialCart
			var materialCart = MaterialListSession;

			//move item from taskCart(ViewModel) to CustomQuotationTask(Model) to add to database 
			List<TaskDetail> taskDetails = taskCart.Select(t => new TaskDetail
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

			List<CustomQuotationBillViewModel> customQuotationBillVMList = new();
			CustomQuotationBillViewModel bill = CalculateQuotationTotalPrice(CustomQuotationSession.Id, taskDetails, materialDetails);

			customQuotationBillVMList.Add(bill);
			return Json(new { data = customQuotationBillVMList });
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetReasonRejected()
		{
			try
			{
				var pathCreater = new PathCreater(_environment);
				string targetFolder = pathCreater.CreateFilePathInRoot(CustomQuotationSession.Id.Trim() + ".txt", "note-reject-quotation-file");
				var reasons = FileManipulater<RejectQuotationDetail>.LoadJsonFromFile(targetFolder);
				var reason = reasons.LastOrDefault();

				List<ReasonRejectViewModel> taskRejectReason = reason.TaskDetailNotes.Select(x => new ReasonRejectViewModel
				{
					Id = x.Key,
					Name = _unitOfWork.Task.GetName(x.Key),
					Reason = x.Value,
				}).ToList();

				List<ReasonRejectViewModel> materialRejectReason = reason.MaterialDetailNotes.Select(x => new ReasonRejectViewModel
				{
					Id = x.Key,
					Name = _unitOfWork.Material.GetName(x.Key),
					Quantity = x.Value.Quantity,
					Reason = x.Value.Note,
				}).ToList();

				List<ReasonRejectViewModel> reasonRejects = taskRejectReason.Concat(materialRejectReason).ToList();
				return Json(new { data = reasonRejects });
			}
			catch
			{
				return Json(new { data = "" });
			}
		}

		#endregion ============ API ============



		#region ============ ACTIONS ============
>>>>>>> Demostration


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

<<<<<<< HEAD
=======

>>>>>>> Demostration
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

<<<<<<< HEAD
=======

>>>>>>> Demostration
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Rejected()
		{
			//Remove all session
			HttpContext.Session.Remove(SessionConst.TASK_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.MATERIAL_LIST_KEY);
			HttpContext.Session.Remove(SessionConst.CUSTOM_QUOTATION_KEY);
			return View();
		}

<<<<<<< HEAD
=======

>>>>>>> Demostration
		/// <summary>
		/// This function return a form to add Task and Material to CustomQuotation represent CustomQuotationTask and MaterialDetail.
		/// </summary>
		/// <returns>A view create quotation form</returns>
		public async Task<IActionResult> Quote(string QuotationId)
		{
			//Update RecieveDateEngineer 
<<<<<<< HEAD
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
=======
			try
			{
				var customQuotation = _unitOfWork.CustomQuotation
					.Get(x => x.Id == QuotationId);
				var workingReport = _unitOfWork.WorkingReport
					.Get(wr => wr.StaffId == GetCurrentUserId() && wr.RequestId == customQuotation.RequestId);
				if (workingReport != null)
					if (workingReport.ReceiveDate == null)
						workingReport.ReceiveDate = DateTime.Now;
				_unitOfWork.WorkingReport.Update(workingReport);
				_unitOfWork.Save();
			}
			catch (Exception)
			{
				TempData["Error"] = $"Có lỗi xảy ra, vui lòng thử lại sau!";
				return RedirectToAction("Index", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Declare constructDetail get data form Database by using _unitOfWork
			var constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Check if constructDetail or customQuotationViewModel. Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null)
			{
				TempData["Error"] = $"Không tìm thấy báo giá!";
>>>>>>> Demostration
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
<<<<<<< HEAD
				taskCart = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id, includeProp: "Task").Select(x => new CustomQuotationTaskViewModel
=======
				taskCart = _unitOfWork.TaskDetail.GetTaskDetail(CustomQuotationSession.Id, includeProp: "Task").Select(x => new TaskDetailViewModel
>>>>>>> Demostration
				{
					Task = x.Task,
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
				materialCart = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id, includeProp: "Material").Select(x => new MaterialDetailViewModel
				{
					Material = x.Material,
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
<<<<<<< HEAD
=======
		/// This function return a form to add Task and Material to CustomQuotation represent CustomQuotationTask and MaterialDetail.
		/// </summary>
		/// <returns>A view create quotation form</returns>
		public async Task<IActionResult> Retake(string QuotationId)
		{
			//Update RecieveDateEngineer 
			try
			{
				var customQuotation = _unitOfWork.CustomQuotation
					.Get(x => x.Id == QuotationId);
				var workingReport = _unitOfWork.WorkingReport
					.Get(wr => wr.StaffId == GetCurrentUserId() && wr.RequestId == customQuotation.RequestId);
				if (workingReport != null)
					if (workingReport.ReceiveDate == null)
						workingReport.ReceiveDate = DateTime.Now;
				_unitOfWork.WorkingReport.Update(workingReport);
				_unitOfWork.Save();
			}
			catch (Exception)
			{
				TempData["Error"] = $"Có lỗi xảy ra, vui lòng thử lại sau!";
				return RedirectToAction("Index", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//Declare constructDetail get data form Database by using _unitOfWork
			var constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Check if constructDetail or customQuotationViewModel. Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null)
			{
				TempData["Error"] = $"Không tìm thấy báo giá!";
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
				taskCart = _unitOfWork.TaskDetail.GetTaskDetail(CustomQuotationSession.Id, includeProp: "Task").Select(x => new TaskDetailViewModel
				{
					Task = x.Task,
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
				materialCart = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id, includeProp: "Material").Select(x => new MaterialDetailViewModel
				{
					Material = x.Material,
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
>>>>>>> Demostration
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
<<<<<<< HEAD
				TempData["Error"] = $"Task list of quote is empty";
=======
				TempData["Error"] = $"Danh sách công việc còn trống";
>>>>>>> Demostration

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//if materialCart == null mean the materialCart have no material in there
			if (materialCart.Count == 0)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
<<<<<<< HEAD
				TempData["Error"] = $"Material list of quote is empty";
=======
				TempData["Error"] = $"Danh sách vật tư còn trống!";
>>>>>>> Demostration

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

			//try catch block to catch and resolve error if it occur
			try
			{
				//move item from taskCart(ViewModel) to CustomQuotationTask(Model) to add to database 
<<<<<<< HEAD
				List<CustomQuotationTask> customQuotaionTasks = taskCart.Select(t => new CustomQuotationTask
=======
				List<TaskDetail> taskDetails = taskCart.Select(t => new TaskDetail
>>>>>>> Demostration
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
<<<<<<< HEAD
				var customQuotationTasksBeDelete = _unitOfWork.CustomQuotaionTask.GetTaskDetail(CustomQuotationSession.Id);
				//Detele the old data after get in database
				_unitOfWork.CustomQuotaionTask.RemoveRange(customQuotationTasksBeDelete);
				//Addrange of customQuotaionTasks to database
				_unitOfWork.CustomQuotaionTask.AddRange(customQuotaionTasks);
=======
				var customQuotationTasksBeDelete = _unitOfWork.TaskDetail.GetTaskDetail(CustomQuotationSession.Id);
				//Detele the old data after get in database
				_unitOfWork.TaskDetail.RemoveRange(customQuotationTasksBeDelete);
				//Addrange of taskDetails to database
				_unitOfWork.TaskDetail.AddRange(taskDetails);
>>>>>>> Demostration

				//Get data from database to delete
				var materialDetailsBeDelete = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id);
				//Delete the old data after get in database
				_unitOfWork.MaterialDetail.RemoveRange(materialDetailsBeDelete);
				//Addrange of materialDetails to database
				_unitOfWork.MaterialDetail.AddRange(materialDetails);

<<<<<<< HEAD
				var total = materialCart.Sum(x => x.Price) + taskCart.Sum(x => x.Price);

				//update total price of customQuotation after submit
				var customQuotation = _unitOfWork.CustomQuotation.Get(x => x.Id == CustomQuotationSession.Id);
				customQuotation.Total = (decimal)total;
				customQuotation.SubmissionDateEngineer = DateTime.Now;
=======
				//update total price of customQuotation after submit
				var customQuotation = _unitOfWork.CustomQuotation.Get(x => x.Id == CustomQuotationSession.Id);

				//Calculate Total of the Quotation
				customQuotation.Total = CalculateQuotationTotalPrice(CustomQuotationSession.Id, taskDetails, materialDetails).TotalPrice;

>>>>>>> Demostration
				_unitOfWork.CustomQuotation.Update(customQuotation);

				//Savechange the database after addrange
				_unitOfWork.Save();

				//Remove all session
				HttpContext.Session.Remove(SessionConst.TASK_LIST_KEY);
				HttpContext.Session.Remove(SessionConst.MATERIAL_LIST_KEY);
				HttpContext.Session.Remove(SessionConst.CUSTOM_QUOTATION_KEY);

				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
<<<<<<< HEAD
				TempData["Success"] = $"Submit quote successfully";

				//Return back to Index of QuotationController
				return View("Index", "Quotation");
=======
				TempData["Success"] = $"Lưu báo giá thành công";

				//Return back to Index of QuotationController
				if (customQuotation.Status == SD.Rejected)
				{
					return RedirectToAction("Rejected", "Quotation");
				}

				return RedirectToAction("Index", "Quotation");
>>>>>>> Demostration
			}
			catch (Exception)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
<<<<<<< HEAD
				TempData["Error"] = $"Something went wrong";
=======
				TempData["Error"] = $"Có lỗi xảy ra, vui lòng thử lại sau";
>>>>>>> Demostration

				//Return back to the QuotationController with action Quote and pass a QuotationId get from CustomQuotationSession
				return RedirectToAction("Quote", "Quotation", new { QuotationId = CustomQuotationSession.Id });
			}

		}

<<<<<<< HEAD

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


=======
		/// <summary>
		/// This function get detail of quotation that was send to manager
		/// </summary>
		/// <param name="QuotationId"></param>
		/// <returns></returns>
>>>>>>> Demostration
		public async Task<IActionResult> Detail(string QuotationId)
		{
			//Declare constructDetail get data form Database by using _unitOfWork
			ConstructDetail? constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null)
			{
				//Return error message to front-end show for customer. the scripts in ~/View/Shared/_Notification.cshml
<<<<<<< HEAD
				TempData["Error"] = $"Quotation not found";
=======
				TempData["Error"] = $"Không tìm thấy báo giá!";
>>>>>>> Demostration

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

<<<<<<< HEAD
			return View(constructDetailVM);
		}

=======
			//Asign TaskListSession for taskCart;
			var taskCart = TaskListSession;

			//if taskCart == null mean the taskCart have no task in there
			if (taskCart.Count == 0)
			{
				taskCart = _unitOfWork.TaskDetail.GetTaskDetail(CustomQuotationSession.Id, includeProp: "Task").Select(x => new TaskDetailViewModel
				{
					Task = x.Task,
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
				materialCart = _unitOfWork.MaterialDetail.GetMaterialDetail(CustomQuotationSession.Id, includeProp: "Material").Select(x => new MaterialDetailViewModel
				{
					Material = x.Material,
					QuotationId = x.QuotationId,
					Quantity = x.Quantity,
					Price = x.Price,
				}).ToList();
			}

			//Update MaterialListSession with materialCart  
			HttpContext.Session.Set(SessionConst.MATERIAL_LIST_KEY, materialCart);

			return View(constructDetailVM);
		}


		#endregion ============ ACTIONS ============



		#region ============ FUNCTIONS ============


		/// <summary>
		/// This function calculate the total of quotation
		/// </summary>
		/// <param name="QuotationId">Id of quotation</param>
		/// <param name="taskDetails">Task list of quotation</param>
		/// <param name="materialDetails">Material list of quotation</param>
		/// <returns></returns>
		public CustomQuotationBillViewModel CalculateQuotationTotalPrice(string QuotationId, List<TaskDetail> taskDetails, List<MaterialDetail> materialDetails)
		{
			var result = new CustomQuotationBillViewModel();

			//Get contrucdetail to gain infor for calculated
			ConstructDetail constructDetail = _unitOfWork.ConstructDetail.Get(x => x.QuotationId == QuotationId, includeProperties: "Foundation,Rooftop,Basement");

			//Get price on one meter.
			result.PriceOnAcreage = (decimal)_unitOfWork.Pricing.Get(x => x.ConstructTypeId == constructDetail.ConstructionId && x.InvestmentTypeId == constructDetail.InvestmentId).UnitPrice;

			//Get acreage
			result.Acreage = (decimal)(constructDetail.Length * constructDetail.Width);

			//Get total foundation acreage.
			result.FoundationAcreage = (decimal)(result.Acreage * constructDetail.Foundation.AreaFactor);

			//Get total basement acreage.
			result.BasementAcreage = (decimal)(result.Acreage * constructDetail.Basement.AreaFactor);

			//Get total balcony acreage.
			result.BalconyAcreage = (constructDetail.Balcony == false) ? 0 : ((decimal)0.3 * (decimal)(constructDetail.Floor * result.Acreage));

			//Get total of all material that necessary
			result.TotalPriceMaterial = (decimal)materialDetails.Sum(x => x.Price);

			//Get total of all task that necessary
			result.TotalPriceTask = (decimal)taskDetails.Sum(x => x.Price);

			//Get total rooftop acreage
			result.RooftopAcreage = (decimal)(result.Acreage * constructDetail.Rooftop.AreaFactor);

			//Get the total acreage of rooftop, balcony, basement, foundation.
			result.TotalAcreage = (decimal)(result.Acreage * constructDetail.Floor + constructDetail.Mezzanine + constructDetail.RooftopFloor + constructDetail.Garden + result.FoundationAcreage + result.BalconyAcreage + result.RooftopAcreage + result.BasementAcreage);

			//result equal to price on 1 meter multiply with total acreage and plus with price of tasks and materials
			result.TotalPrice = result.PriceOnAcreage * result.TotalAcreage + result.TotalPriceMaterial + result.TotalPriceTask;

			//return result after calculated
			return result;
		}


		/// <summary>
		/// This function get the Id of user while logging in
		/// </summary>
		/// <returns></returns>
		private string GetCurrentUserId()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}


		#endregion ============ FUNCTIONS 


>>>>>>> Demostration
	}
}
