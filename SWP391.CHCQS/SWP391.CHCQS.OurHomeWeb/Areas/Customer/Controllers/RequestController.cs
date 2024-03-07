using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Services;
using SWP391.CHCQS.Utility;
using System.Net.WebSockets;
using System.Security.Claims;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class RequestController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
		public RequestController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
			_userManager = userManager;
        }
        public async Task<IActionResult> Index()
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> CreateRequest()
		{
			RequestVM requestVM = new()
			{
				ConstructionTypes = _unitOfWork.ConstructionType.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Name
				})
			};
			return View(requestVM);
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateRequest(RequestVM requestVM)
		{
			RequestForm requestForm = new() { 
				Id = CreateRequestId(),
				GenerateDate = DateTime.Now,
				Status = SD.RequestStatusPending,
				Description = requestVM.Description,
				ConstructType = requestVM.ConstructType,
				Acreage = requestVM.Acreage,
				Location = requestVM.Location

			};

			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			requestForm.CustomerId = userId;
			requestForm.Customer = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);			

			_unitOfWork.RequestForm.Add(requestForm);
			_unitOfWork.Save();

			var delegationService = AppState.Instance(_userManager).GetDelegationIndex();
			var sellerId = _userManager.GetUsersInRoleAsync(SD.Role_Seller)
				.GetAwaiter().GetResult()
				.SkipWhile((entity,index) => index < delegationService.Item1 -1)
				.FirstOrDefault().Id;
			var engineerId = _userManager.GetUsersInRoleAsync(SD.Role_Engineer)
				.GetAwaiter().GetResult()
				.SkipWhile((entity, index) => index < delegationService.Item2 -1)
				.FirstOrDefault().Id;
			var managerId = _userManager.GetUsersInRoleAsync(SD.Role_Manager)
				.GetAwaiter().GetResult()
				.SkipWhile((entity, index) => index < delegationService.Item3 -1)
				.FirstOrDefault().Id;
			var sellerReport = new WorkingReport
			{
				RequestId = requestForm.Id,
				StaffId = sellerId,
			};
			var engineerReport = new WorkingReport
			{
				RequestId = requestForm.Id,
				StaffId = engineerId,
			};
			var managerReport = new WorkingReport
			{
				RequestId = requestForm.Id,
				StaffId = managerId,
			};
			_unitOfWork.WorkingReport.Add(sellerReport);
			_unitOfWork.WorkingReport.Add(engineerReport);
			_unitOfWork.WorkingReport.Add(managerReport);
			_unitOfWork.Save();

			TempData["Success"] = "Request has been sent successfully";
			return RedirectToAction(nameof(RequestHistory));
		}

		
		[Authorize]
		public async Task<IActionResult> RequestHistory()
		{
			
			return View();
		}
		[Authorize]
		public async Task<IActionResult> ViewResponse(string id)
		{
			QuotationVM quotationVM = new();
			var quotation = _unitOfWork.CustomQuotation
				.Get(t => t.RequestId == id && t.Status == SD.Completed, 
				includeProperties: "ConstructDetail,TaskDetails,MaterialDetails");
			if (quotation != null)
			{
				quotationVM.Id = quotation.Id;
				quotationVM.ConstructionType = quotation.ConstructDetail.Construction.Name;
				quotationVM.InvestmentType = quotation.ConstructDetail.Investment.Name;
				quotationVM.FoundationType = quotation.ConstructDetail.Foundation.Name;
				quotationVM.RoofType = quotation.ConstructDetail.Rooftop.Name;
				quotationVM.BasementType = quotation.ConstructDetail.Basement.Name;
				quotationVM.Width = quotation.ConstructDetail.Width;
				quotationVM.Lenght = quotation.ConstructDetail.Length;
				quotationVM.Facade = quotation.ConstructDetail.Facade;
				quotationVM.Alley = quotation.ConstructDetail.Alley;
				quotationVM.Floor = quotation.ConstructDetail.Floor;
				quotationVM.Mezzanine = quotation.ConstructDetail.Mezzanine;
				quotationVM.RooftopFloor = quotation.ConstructDetail.RooftopFloor;
				quotationVM.Balcony = quotation.ConstructDetail.Balcony;
				quotationVM.Garden = quotation.ConstructDetail.Garden;
				quotationVM.Description = quotation.Description;
				quotationVM.TotalPrice = quotation.Total;
				quotationVM.Materials = quotation.MaterialDetails.ToList();
				quotationVM.Tasks = quotation.TaskDetails.ToList();
			}
			if (quotationVM.Id == null)
			{
				TempData["Error"] = "No response found";
				return RedirectToAction(nameof(RequestHistory));
			}
			return View(quotationVM);
		}

		public string CreateRequestId()
		{
			return SD.requestIdKey + String.Format("{0:D3}", _unitOfWork.RequestForm.GetAll().Count() + 1);
		}

		#region API CALLS
		[HttpGet]
		public async Task<IActionResult> GetRequestHistory()
		{
			var count = 0;
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			List<RequestVM> RequestList = _unitOfWork.RequestForm
				.GetAll(t => t.CustomerId == userId)
				.Select(t => new RequestVM
				{
					RequestId = t.Id,
					NumberOfOrder = ++count,
					GenerateDate = t.GenerateDate.ToShortDateString(),
					Description = t.Description,
					ConstructType = t.ConstructType,
					Acreage = t.Acreage,
					Location = t.Location,
					Status = t.Status
				})
				.ToList();

			return Json(new { data = RequestList });
		}
		#endregion
	}
}
