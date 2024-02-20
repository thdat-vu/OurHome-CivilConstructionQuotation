using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers;
using SWP391.CHCQS.Utility;
using System.Collections.Generic;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Engineer.Controllers
{
	[Area("Engineer")]
	public class QuotationController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		public QuotationController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region
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
		/// This function return a form to create new Quotation
		/// </summary>
		/// <returns>A view create quotation form</returns>
		public async Task<IActionResult> Quote(string QuotationId)
		{
			ConstructDetail? constructDetail = _unitOfWork.ConstructDetail.Get(filter: c => c.QuotationId == QuotationId, includeProperties: "Construction,Investment,Foundation,Rooftop,Basement");
			ConstructDetailViewModel constructDetailVM;

			if (constructDetail == null)
			{
				return RedirectToAction("Error", "Home");
			}
			else
			{
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
