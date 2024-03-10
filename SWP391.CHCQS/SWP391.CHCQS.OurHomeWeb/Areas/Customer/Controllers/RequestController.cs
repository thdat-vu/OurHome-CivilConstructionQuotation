﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Services;
using SWP391.CHCQS.Services.NotificationHub;
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
		private readonly IHubContext<NotificationHub> _hubContext;
		public RequestController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IHubContext<NotificationHub> hubContext)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
			_hubContext = hubContext;
		}
		public async Task<IActionResult> QuickQuote()
		{
			var quickQuote = new QuickQuoteVM();

			quickQuote.BasementTypes = _unitOfWork.BasementType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.ConstructionTypes = _unitOfWork.ConstructionType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.FoundationTypes = _unitOfWork.FoundationType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.InvestmentTypes = _unitOfWork.InvestmentType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.RooftopTypes = _unitOfWork.RoofType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.Alleys = new List<SelectListItem>();
			SD.Alleys.ForEach(i =>
			{
				quickQuote.Alleys.Add(new SelectListItem
				{
					Text = i,
					Value = i
				});
			});

			return View(quickQuote);
		}

		[HttpPost]
		[ActionName("QuickQuote")]
		public IActionResult QuickQuotePost(QuickQuoteVM quickQuote)
		{
			quickQuote.ConstructDetail.Construction = _unitOfWork.ConstructionType.Get(x => x.Id == quickQuote.ConstructDetail.ConstructionId);
			quickQuote.ConstructDetail.Foundation = _unitOfWork.FoundationType.Get(x => x.Id == quickQuote.ConstructDetail.FoundationId);
			quickQuote.ConstructDetail.Investment = _unitOfWork.InvestmentType.Get(x => x.Id == quickQuote.ConstructDetail.InvestmentId);
			quickQuote.ConstructDetail.Rooftop = _unitOfWork.RoofType.Get(x => x.Id == quickQuote.ConstructDetail.RooftopId);
			quickQuote.ConstructDetail.Basement = _unitOfWork.BasementType.Get(x => x.Id == quickQuote.ConstructDetail.BasementId);

			quickQuote.ResponseBill = GetBill(quickQuote);

			quickQuote.BasementTypes = _unitOfWork.BasementType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.ConstructionTypes = _unitOfWork.ConstructionType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.FoundationTypes = _unitOfWork.FoundationType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.InvestmentTypes = _unitOfWork.InvestmentType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.RooftopTypes = _unitOfWork.RoofType.GetAll().Select(i => new SelectListItem
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
			quickQuote.Alleys = new List<SelectListItem>();
			SD.Alleys.ForEach(i =>
			{
				quickQuote.Alleys.Add(new SelectListItem
				{
					Text = i,
					Value = i
				});
			});

			return View(quickQuote);
		}

		[Authorize(Roles = SD.Role_Customer)]
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
		[Authorize(Roles = SD.Role_Customer)]
		[HttpPost]
		public async Task<IActionResult> CreateRequest(RequestVM requestVM)
		{
			RequestForm requestForm = new()
			{
				Id = CreateRequestId(),
				GenerateDate = DateTime.Now,
				Status = SD.RequestStatusPending,
				Description = requestVM.Description,
				ConstructType = requestVM.ConstructType,
				Acreage = requestVM.Acreage,
				Location = requestVM.Location

			};

			requestForm.CustomerId = SD.GetCurrentUserId(User);
			requestForm.Customer = _unitOfWork.ApplicationUser.Get(u => u.Id == requestForm.CustomerId);

			_unitOfWork.RequestForm.Add(requestForm);
			_unitOfWork.Save();

			DelegateRequest(requestForm.Id);

			_hubContext.Clients.All.SendAsync("RecieveRequestFromCustomer");
			TempData["Success"] = "Request has been sent successfully";
			return RedirectToAction(nameof(RequestHistory));
		}


		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> RequestHistory()
		{

			return View();
		}
		[Authorize(Roles = SD.Role_Customer)]
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

		#region FUNCTIONS
		public void DelegateRequest(string requestId)
		{
			var delegationService = AppState.Instance(_userManager).GetDelegationIndex();
			var sellerId = _userManager.GetUsersInRoleAsync(SD.Role_Seller)
				.GetAwaiter().GetResult()
				.SkipWhile((entity, index) => index < delegationService.Item1 - 1)
				.FirstOrDefault().Id;
			var engineerId = _userManager.GetUsersInRoleAsync(SD.Role_Engineer)
				.GetAwaiter().GetResult()
				.SkipWhile((entity, index) => index < delegationService.Item2 - 1)
				.FirstOrDefault().Id;
			var managerId = _userManager.GetUsersInRoleAsync(SD.Role_Manager)
				.GetAwaiter().GetResult()
				.SkipWhile((entity, index) => index < delegationService.Item3 - 1)
				.FirstOrDefault().Id;
			var sellerReport = new WorkingReport
			{
				RequestId = requestId,
				StaffId = sellerId,
			};
			var engineerReport = new WorkingReport
			{
				RequestId = requestId,
				StaffId = engineerId,
			};
			var managerReport = new WorkingReport
			{
				RequestId = requestId,
				StaffId = managerId,
			};
			_unitOfWork.WorkingReport.Add(sellerReport);
			_unitOfWork.WorkingReport.Add(engineerReport);
			_unitOfWork.WorkingReport.Add(managerReport);
			_unitOfWork.Save();
		}

		public string CreateRequestId()
		{
			return SD.requestIdKey + String.Format("{0:D3}", _unitOfWork.RequestForm.GetAll().Count() + 1);
		}

		public Bill GetBill(QuickQuoteVM quickQuote)
		{
			var count = 1;
			var detail = quickQuote.ConstructDetail;
			var area = detail.Width * detail.Length;
			var unitPrice = (double)_unitOfWork.Pricing.Get(x => x.ConstructTypeId == detail.ConstructionId && x.InvestmentTypeId == detail.InvestmentId).UnitPrice;
			if (detail.Alley == SD.Alleys[0])//Wider than 5m
			{

			}
			else if (detail.Alley == SD.Alleys[1])//Width from 3m - 5m
			{
				unitPrice += SD.AlleySurcharge;
			}
			else//Less than 3m
			{
				unitPrice += (2 * SD.AlleySurcharge);
			}

			Bill bill = new Bill()
			{
				TotalArea = 0,
				TotalPrice = 0,
				UnitPrice = unitPrice,
				BillDetails = new List<BillDetail>()
				{
					new ()
					{
						NumberOfOrder =  count++,
						Type = detail.Foundation.Name,//loại móng
						ConstructionArea =(double) (detail.Foundation.AreaFactor * area)
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = "Lửng",
						ConstructionArea= (double) (detail.Mezzanine)
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = "Thông Lửng",
						ConstructionArea= (double) (area - detail.Mezzanine) * SD.MezzanineCoefficient
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = "Tầng thượng",
						ConstructionArea= (double) (detail.RooftopFloor)
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = "Sân thượng",
						ConstructionArea= (double) (area - detail.RooftopFloor) * SD.TerraceCoefficient
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = detail.Rooftop.Name, //loại mái
						ConstructionArea= (double) (detail.Rooftop.AreaFactor * area)
					},
					new ()
					{ NumberOfOrder =  count++,
						Type = "Sân vườn + Móng sân ",
						ConstructionArea= (double) (detail.Garden) * SD.GardenCoefficient
					},
					new ()
					{
						NumberOfOrder =  count++,
						Type = "Tầng trệt",
						ConstructionArea= (double)area
					},
				}
			};
			if (detail.Floor > 1)//Nhà có nhiều tầng không tính trệt
			{
				for (int i = 1; i < detail.Floor; i++)
				{
					bill.BillDetails.Add(new()
					{
						NumberOfOrder = count++,
						Type = "Tầng " + i,
						ConstructionArea = (double)area
					});
				}
				if (detail.Balcony)
				{
					bill.BillDetails.Add(new()
					{
						NumberOfOrder = count++,
						Type = "Ban công cho " + (detail.Floor - 1) + " tầng",
						ConstructionArea = (detail.Floor - 1) * SD.DefaultBalconyArea
					});
				}
			}
			if (detail.Basement.Id != SD.NoBasementId)// có hầm
			{
				bill.BillDetails.Add(new()
				{
					NumberOfOrder = count++,
					Type = "Hầm " + detail.Basement.Name,
					ConstructionArea = (double)(detail.Basement.AreaFactor * area)
				});
			}
			//tính giá từng loại chi tiết => rồi tính tổng giá
			bill.BillDetails.ForEach(i => { 
				i.Price = i.ConstructionArea * (double)bill.UnitPrice;
				bill.TotalPrice += i.Price;
			}) ;
			//tính tổng diện tích
			bill.BillDetails.ForEach(i => bill.TotalArea += i.ConstructionArea);
			return bill;
		}

		#endregion
	}
}
