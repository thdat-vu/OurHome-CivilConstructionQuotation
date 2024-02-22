using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class QuotationController : Controller
	{
		//Declare _uniteOfWork represent to DBContext to get Data form Database.
		private readonly IUnitOfWork _unitOfWork;

		//Declare Session to store CustomQuotation serve to method AddToList in TaskController and MaterialController to add Task and Material.
		public CustomQuotationViewModel CustomQuotationSession => HttpContext.Session.Get<CustomQuotationViewModel>(SessionConst.CUSTOM_QUOTATION_KEY) ?? new CustomQuotationViewModel();

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
		public IActionResult GetAll()
		{
			List<CustomQuotationViewModel> customQuotationVMList = _unitOfWork.CustomQuotation
				.GetAll()
				.OrderBy(x => x.Status == SD.Processing)
				.Select(x => new CustomQuotationViewModel
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
			return View();
		}

		/// <summary>
		/// This function return a form to add Task and Material to CustomQuotation represent CustomQuotationTask and MaterialDetail.
		/// </summary>
		/// <returns>A view create quotation form</returns>
		public async Task<IActionResult> Quote(string QuotationId)
		{

			//Declare constructDetail get data form Database by using _unitOfWork
			ConstructDetail? constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");

			//Declare view model to pass to View.
			ConstructDetailViewModel constructDetailVM;

			//Declare view model to set into Session CustomQuotationSession
			CustomQuotationViewModel customQuotationViewModel = new CustomQuotationViewModel();

			//Get only id of customQuotationViewMode from database by using _unitOfWork
			customQuotationViewModel.Id = _unitOfWork.CustomQuotation.Get(x => x.Id == QuotationId).Id;

			//Check if constructDetail or customQuotationViewModel.Id not in database is true, it return error view. If not, is will execute next code.
			if (constructDetail == null || customQuotationViewModel.Id == null)
			{
				return RedirectToAction("Error", "Home");
			}
			else
			{
				//projection data constructDetail to constructDetailVM
				constructDetailVM = new ConstructDetailViewModel
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
			}

			//Set customQuotationViewModel after exist in database into CustomQuotationSession
			HttpContext.Session.Set(SessionConst.CUSTOM_QUOTATION_KEY, customQuotationViewModel);

			//return View of this Controller after nothing wrong.
			return View(constructDetailVM);
		}

		/// <summary>
		/// This function return a form to edit exist quotation
		/// </summary>
		/// <param name="id">Id of the quotation that be selected</param>
		/// <returns>Return a form with detail of the quotation to edit</returns>
		public async Task<IActionResult> Edit(string QuotationId)
		{
			return View();
		}
	}
}
