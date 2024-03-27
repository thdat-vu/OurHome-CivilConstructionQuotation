using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Services.NotificationHub;
using SWP391.CHCQS.Utility;
using System.Text.Json;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = SD.Role_Seller)]
    public class QuotationController : Controller
    {
        #region ============ DECLARE ============
        //Declare _uniteOfWork represent to DBContext to get Data form Database.
        private readonly IUnitOfWork _unitOfWork;

        //Declare NotificationHub
        private readonly IHubContext<NotificationHub> _hubContext;

        //Constructor of this Controller
        public QuotationController(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }
        #endregion ============ DECLARE ============


        #region ============ ACTIONS ============

        /// <summary>
        /// This function enable for user create a quotation and add construct detail into DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateConstructDetails(string? id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(u => u.Id == id);
            //phải có thằng này vì nó có khóa chung giữa 2 bảng là ConstructDetail và CustomQuotation, nếu muốn thêm thì phải thêm vào cả 2
            //hiểu nôm na là phải có CustomQuotation id trước khi có thằng ConstructDetail id
            var customQuotation = new CustomQuotation
            {
                Id = CreateQuotationId(id),
                Date = DateTime.Now,
                Acreage = requestForm.Acreage,
                Location = requestForm.Location,
                Status = SD.Preparing,
                Description = requestForm.Description,
                Total = 0,
                RequestId = requestForm.Id
            };
            var serializedData = JsonSerializer.Serialize(customQuotation);
            ViewBag.CustomQuotation = serializedData;

            //new mới một object lấy ra những option với những id tương ứng
            ConstructDetailViewModel ConstructDetailVM = new()
            {

                Basement = _unitOfWork.BasementType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id,
                }),
                Construction = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id,
                }),
                Foundation = _unitOfWork.FoundationType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id,
                }),
                Investment = _unitOfWork.InvestmentType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id,
                }),
                Rooftop = _unitOfWork.RoofType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id,
                }),
                Alleys = SD.Alleys.Select(x => new SelectListItem { Text = x, Value = x }).ToList(),
                Facades = SD.Facades.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList(),
                QuotationId = customQuotation.Id 
            };
            return View(ConstructDetailVM);
        }

        [HttpPost]
        [ActionName("CreateConstructDetails")]
        public IActionResult CreateConstructDetailsPost(ConstructDetailViewModel constructDetailVM, string hiddenData)
        {
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng ConstructDetail mới với các giá trị đã chọn từ ViewModel
                CustomQuotation? customQuotation = JsonSerializer.Deserialize<CustomQuotation>(hiddenData);

                //***********BUG***********
                //customQuotation.SubmissionDateSeller = DateTime.Now;
                //***********BUG***********
               
                ConstructDetail constructDetail = new ConstructDetail
                {
                    ConstructionId = constructDetailVM.ConstructDetail.ConstructionId,
                    InvestmentId = constructDetailVM.ConstructDetail.InvestmentId,
                    Width = constructDetailVM.ConstructDetail.Width,
                    Length = constructDetailVM.ConstructDetail.Length,
                    BasementId = constructDetailVM.ConstructDetail.BasementId,
                    Alley = constructDetailVM.ConstructDetail.Alley,
                    Balcony = constructDetailVM.ConstructDetail.Balcony,
                    Facade = constructDetailVM.ConstructDetail.Facade,
                    Floor = constructDetailVM.ConstructDetail.Floor,
                    Garden = constructDetailVM.ConstructDetail.Garden,
                    Mezzanine = constructDetailVM.ConstructDetail.Mezzanine,
                    Room = constructDetailVM.ConstructDetail.Room,
                    FoundationId = constructDetailVM.ConstructDetail.FoundationId,
                    RooftopId = constructDetailVM.ConstructDetail.RooftopId,
                    RooftopFloor = constructDetailVM.ConstructDetail.RooftopFloor,
                    QuotationId = customQuotation.Id
                  
                };
                // Thêm ConstructDetail vào unit of work và lưu thay đổi
                _unitOfWork.CustomQuotation.Add(customQuotation);
                _unitOfWork.ConstructDetail.Add(constructDetail);

                var requestForm = _unitOfWork.RequestForm.Get(x => x.Id == customQuotation.RequestId);
                requestForm.Status = SD.RequestStatusSaved;
                _unitOfWork.RequestForm.Update(requestForm);

                _unitOfWork.Save();


				TempData["success"] = "Tạo thông tin chi tiết công trình thành công";
                _hubContext.Clients.All.SendAsync("RecieveQuotationFromSeller");
                return RedirectToAction("Index", "Request");
            }
            else
            {
                ConstructDetailViewModel ConstructDetailVM = new()
                {
                    Basement = _unitOfWork.BasementType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Construction = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Foundation = _unitOfWork.FoundationType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Investment = _unitOfWork.InvestmentType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Rooftop = _unitOfWork.RoofType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Alleys = SD.Alleys.Select(x => new SelectListItem { Text = x, Value = x }).ToList(),
                    Facades = SD.Facades.Select(x => new SelectListItem { Text = x.ToString(), Value = x.ToString() }).ToList()
                };
                TempData["error"] = "Tạo thông tin chi tiết công trình thất bại";
                return View(ConstructDetailVM);
            }

        }

        [HttpPost]
        public IActionResult UpdateConstructDetails(ConstructDetailViewModel constructDetailVM)
        {
            if (ModelState.IsValid)
            {

                ConstructDetail constructDetail = new ConstructDetail
                {
                    ConstructionId = constructDetailVM.ConstructDetail.ConstructionId,
                    InvestmentId = constructDetailVM.ConstructDetail.InvestmentId,
                    Width = constructDetailVM.ConstructDetail.Width,
                    Length = constructDetailVM.ConstructDetail.Length,
                    BasementId = constructDetailVM.ConstructDetail.BasementId,
                    Alley = constructDetailVM.ConstructDetail.Alley,
                    Balcony = constructDetailVM.ConstructDetail.Balcony,
                    Facade = constructDetailVM.ConstructDetail.Facade,
                    Floor = constructDetailVM.ConstructDetail.Floor,
                    Garden = constructDetailVM.ConstructDetail.Garden,
                    Mezzanine = constructDetailVM.ConstructDetail.Mezzanine,
                    Room = constructDetailVM.ConstructDetail.Room,
                    FoundationId = constructDetailVM.ConstructDetail.FoundationId,
                    RooftopId = constructDetailVM.ConstructDetail.RooftopId,
                    RooftopFloor = constructDetailVM.ConstructDetail.RooftopFloor,
                    QuotationId = constructDetailVM.ConstructDetail.QuotationId,

                };
                // lưu thay đổi
                _unitOfWork.ConstructDetail.Update(constructDetail);
                _unitOfWork.Save();


                TempData["success"] = "Thay đổi thông tin chi tiết công trình thành công";
                _hubContext.Clients.All.SendAsync("RecieveQuotationFromSeller");

                
            }
            else
            {
                ConstructDetailViewModel ConstructDetailVM = new()
                {
                    Basement = _unitOfWork.BasementType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Construction = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Foundation = _unitOfWork.FoundationType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Investment = _unitOfWork.InvestmentType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    Rooftop = _unitOfWork.RoofType.GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id,
                    }),
                    ConstructDetail = new ConstructDetail()
                };
                TempData["error"] = "Thay đổi thông tin chi tiết công trình thất bại";                
            }
            //reload lại trang
            var quotation = _unitOfWork.CustomQuotation.Get(x => x.Id == constructDetailVM.ConstructDetail.QuotationId);
            return RedirectToAction("ViewConstructDetails", "Request", new { id = quotation.RequestId });
        }

		public IActionResult ViewQuotation()
		{
			return View();
		}
		public IActionResult Details(string id)
		{
			QuotationViewModel quotationViewModel = new QuotationViewModel
			{
				ConstructDetail = _unitOfWork.ConstructDetail.Get(x => x.QuotationId == id, includeProperties: "Basement,Construction,Foundation,Investment,Rooftop"),
				CustomQuotation = _unitOfWork.CustomQuotation.Get(x => x.Id == id)
			};
			if (quotationViewModel.ConstructDetail != null)
			{
				quotationViewModel.BasementName = quotationViewModel.ConstructDetail.Basement.Name;
				quotationViewModel.RoofName = quotationViewModel.ConstructDetail.Rooftop.Name;
				quotationViewModel.ConstructionName = quotationViewModel.ConstructDetail.Construction.Name;
				quotationViewModel.FoundationName = quotationViewModel.ConstructDetail.Foundation.Name;
				quotationViewModel.InvestmentName = quotationViewModel.ConstructDetail.Investment.Name;
				return View(quotationViewModel);
			}
			else
			{
				TempData["Error"] = "Không tìm thấy báo giá";
				return View(nameof(ViewQuotation));
			}

		}


		#endregion ============ ACTIONS ============

		#region ============ API ============
		/// <summary>
		/// This function get all Quotation in Database and return it into JSON, this function ne lib Datatables to show data
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
            var requestIdList = GetRequestIdList();
			List<QuotationStatusViewModel> CustomQuotationList = _unitOfWork.CustomQuotation
				.GetAll(x => x.Status == SD.Processing)
                .Where(x => requestIdList.Contains(x.RequestId))
                .OrderByDescending(x => x.Date)
                .Select(x => new QuotationStatusViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Acreage = x.Acreage,
                    Location = x.Location,
                    Status = SD.GetQuotationStatusDescription(x.Status),
                    Description = x.Description,
                    
                })
				.ToList();

            //Return Json for datatables to read
            return Json(new { data = CustomQuotationList });

        }
        #endregion============ API ============


        #region ============ FUNCTIONS ============ 
        public List<string> GetRequestIdList()
        {
            var userId = SD.GetCurrentUserId(User);
            return _unitOfWork.WorkingReport.GetAll(x => x.StaffId == userId).Select(x => x.RequestId).ToList();
        }

        public string CreateQuotationId(string requestId)
        {
            string number = requestId.Substring(2);
            return SD.quotationIdKey + number;
		}
        #endregion ============ FUNCTIONS ============
    }
}
