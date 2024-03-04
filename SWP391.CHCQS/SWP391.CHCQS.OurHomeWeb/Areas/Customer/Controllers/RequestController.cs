using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels;
using SWP391.CHCQS.Utility;
using System.Security.Claims;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class RequestController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
        public RequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
				RequestForm = new RequestForm(),
				ConstructionTypes = _unitOfWork.ConstructionType.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				})
			};
			return View(requestVM);
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateRequest(RequestVM requestVM)
		{
			RequestForm requestForm = requestVM.RequestForm;
			requestForm.Id = SD.TempId;
			requestForm.GenerateDate = DateTime.Now;
			requestForm.Status = SD.RequestStatusPending;

			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			requestForm.CustomerId = userId;

			
			TempData["Success"] = "Request has been sent successfully";
			return RedirectToAction(nameof(ManageRequest));
		}

		[Authorize]
		public async Task<IActionResult> ManageRequest()
		{
			return View("RequestHistory");
		}
		[Authorize]
		public async Task<IActionResult> RequestHistory()
		{
			return View();
		}
		[Authorize]
		public async Task<IActionResult> ViewResponse()
		{
			return View();
		}
	}
}
