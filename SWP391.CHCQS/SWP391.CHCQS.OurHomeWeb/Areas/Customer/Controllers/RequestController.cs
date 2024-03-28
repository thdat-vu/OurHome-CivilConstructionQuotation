using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Customer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
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
			quickQuote.Facades = new List<SelectListItem>();
			SD.Facades.ForEach(i =>
			{
				quickQuote.Facades.Add(new SelectListItem
				{
					Text = i.ToString(),
					Value = i.ToString()
				});
			});

			return View(quickQuote);
		}

		[HttpPost]
		[ActionName("QuickQuote")]
		public IActionResult QuickQuotePost(QuickQuoteVM quickQuote)
		{
			if (ModelState.IsValid)
			{
				quickQuote.ConstructDetail.Construction = _unitOfWork.ConstructionType.Get(x => x.Id == quickQuote.ConstructDetail.ConstructionId);
				quickQuote.ConstructDetail.Foundation = _unitOfWork.FoundationType.Get(x => x.Id == quickQuote.ConstructDetail.FoundationId);
				quickQuote.ConstructDetail.Investment = _unitOfWork.InvestmentType.Get(x => x.Id == quickQuote.ConstructDetail.InvestmentId);
				quickQuote.ConstructDetail.Rooftop = _unitOfWork.RoofType.Get(x => x.Id == quickQuote.ConstructDetail.RooftopId);
				quickQuote.ConstructDetail.Basement = _unitOfWork.BasementType.Get(x => x.Id == quickQuote.ConstructDetail.BasementId);

				quickQuote.ResponseBill = GetBill(quickQuote);
			}
			else
			{
				TempData["Error"] = "Thông tin báo giá không hợp lệ";
			}

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
			quickQuote.Alleys = SD.Alleys.Select(i => new SelectListItem { Text = i, Value = i }).ToList();
			quickQuote.Facades = SD.Facades.Select(i => new SelectListItem { 
				Text = i.ToString(), 
				Value = i.ToString() }).ToList();


			return View(quickQuote);
		}

		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> CreateRequest()
		{
			var sellerCount = _userManager.GetUsersInRoleAsync(SD.Role_Seller)
                .GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
                .Count();
			var engineerCount = _userManager.GetUsersInRoleAsync(SD.Role_Engineer)
				.GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
                .Count();
			var managerCount = _userManager.GetUsersInRoleAsync(SD.Role_Manager)
				.GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
                .Count();
			if (sellerCount == 0 || engineerCount == 0 || managerCount == 0)
			{
				TempData["Error"] = "Chức năng hiện không khả dụng";
				return RedirectToAction("Index", "Home");
			}
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
			if (ModelState.IsValid)
			{
				RequestForm requestForm = new()
				{
					Id = CreateRequestId(),
					GenerateDate = DateTime.Now,
					Status = requestVM.Status,
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
				TempData["Success"] = "Yêu cầu báo giá được gửi thành công";
				return RedirectToAction(nameof(RequestHistory));
			}
			TempData["Error"] = "Gửi yêu cầu thất bại";
			//reload lại trang
			requestVM = new()
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
		public async Task<IActionResult> RequestHistory()
		{

			return View();
		}


		[Authorize(Roles = SD.Role_Customer)]
		public async Task<IActionResult> ViewResponse(string id)
		{
			var quotation = _unitOfWork.CustomQuotation
				.Get(t => t.RequestId == id && t.Status == SD.Completed);
				//includeProperties: "Request,ConstructDetail,TaskDetails,MaterialDetails");
			if (quotation != null)
			{
				//đã có phản hồi
				//QuotationVM quotationVM = new()
				//{
				//	Quotation = quotation,
				//	ConstructDetail = quotation.ConstructDetail,
				//	Materials = quotation.MaterialDetails.ToList(),
				//	Tasks = quotation.TaskDetails.ToList(),
				//	Request = quotation.Request
				//};
				//Tiến hành lấy quotation đầy đủ ra
				var info = _unitOfWork.CustomQuotation.Get((x) => x.Id == quotation.Id, "Request,ConstructDetail");

				//Tiến hành fill thông tin cho PDFQuotation
				var pdf = new PDFQuotation
				{
					Id = info.Id,
					Date = info.Date,
					Acreage = info.Acreage,
					Location = info.Location,
					Description = info.Description,
					Total = info.Total,
					GenerateDateRequest = info.Request.GenerateDate,
					ConstructDetail = info.ConstructDetail,
					RequestId = info.RequestId
				};

				//bổ sung construct Detail
				pdf.ConstructDetail.Basement = _unitOfWork.BasementType.Get((x) => x.Id == info.ConstructDetail.BasementId);

				pdf.ConstructDetail.Foundation = _unitOfWork.FoundationType.Get((x) => x.Id == info.ConstructDetail.FoundationId);

				pdf.ConstructDetail.Construction = _unitOfWork.ConstructionType.Get((x) => x.Id == info.ConstructDetail.ConstructionId);

				pdf.ConstructDetail.Investment = _unitOfWork.InvestmentType.Get((x) => x.Id == info.ConstructDetail.InvestmentId);

				pdf.ConstructDetail.Rooftop = _unitOfWork.RoofType.Get((x) => x.Id == info.ConstructDetail.RooftopId);

				var workingReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == info.RequestId);
				foreach (var workReport in workingReport)
				{
					//lấy nhân viên ra
					var staff = await _userManager.FindByIdAsync(info.Request.CustomerId) as ApplicationUser;
					//xác nhận role của nhân viên đó
					var role = await _userManager.GetRolesAsync(staff);
					//gán cho biến name với staff role tương ứng
					if (role.First() == SD.Role_Seller)
						pdf.SellerName = staff.Name;
					if (role.First() == SD.Role_Engineer)
						pdf.EngineerName = staff.Name;
					if (role.First() == SD.Role_Manager)
						pdf.ManagerName = staff.Name;
				}
				//Lỗi đây nè ~ 
				//pdf.CustomerName = _unitOfWork.Customer.Get((x) => x.Id == info.Request.CustomerId).Name;
				//sửa lại lấy dc tên khách hàng ra
				pdf.CustomerName = (_userManager.FindByIdAsync(info.Request.CustomerId).GetAwaiter().GetResult() as ApplicationUser).Name;
				//tiên hành lấy taskdetail và materialdetail
				pdf.Tasks = new List<TaskDetail>(_unitOfWork.TaskDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Task"));
				pdf.Materials = new List<MaterialDetail>(_unitOfWork.MaterialDetail.GetAllWithFilter((x) => x.QuotationId == info.Id, "Material"));

				return View(pdf);
			}
			else
			{
				TempData["Error"] = "Báo giá chưa có phản hồi";
				return RedirectToAction(nameof(RequestHistory));
			}
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

		[NonAction]		
		public void DelegateRequest(string requestId)
		{
			var delegationService = AppState.Instance(_userManager).GetDelegationIndex();

            var sellerId = _userManager.GetUsersInRoleAsync(SD.Role_Seller)
                .GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
				.SkipWhile((entity, index) => index < delegationService.Item1)
				.FirstOrDefault().Id;

			var engineerId = _userManager.GetUsersInRoleAsync(SD.Role_Engineer)
				.GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
                .SkipWhile((entity, index) => index < delegationService.Item2)
				.FirstOrDefault().Id;

			var managerId = _userManager.GetUsersInRoleAsync(SD.Role_Manager)
				.GetAwaiter().GetResult()
                .Where(x => x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.Now)
                .SkipWhile((entity, index) => index < delegationService.Item3)
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

		[NonAction]
		public string CreateRequestId()
		{
			return SD.requestIdKey + String.Format("{0:D3}", _unitOfWork.RequestForm.GetAll().Count() + 1);
		}

		[NonAction]
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
			bill.BillDetails.ForEach(i =>
			{
				i.Price = i.ConstructionArea * (double)bill.UnitPrice;
				bill.TotalPrice += i.Price;
			});
			//tính tổng diện tích
			bill.BillDetails.ForEach(i => bill.TotalArea += i.ConstructionArea);
			return bill;
		}

		#endregion
	}
}
