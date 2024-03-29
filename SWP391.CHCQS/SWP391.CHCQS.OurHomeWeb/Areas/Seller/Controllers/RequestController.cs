using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Services.NotificationHub;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = SD.Role_Seller)]
    public class RequestController : Controller
    {
        #region ============ DECLARE ============
        //Declare _uniteOfWork represent to DBContext to get Data form Database.
        private readonly IUnitOfWork _unitOfWork;

        //Declare NotificationHub
        private readonly IHubContext<NotificationHub> _hubContext;
        public RequestController(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        #endregion ============ DECLARE ============

		#region ============ API ============
		/// <summary>
		/// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var requestIdList = GetWaitingRequestIdList();
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .Where(t => t.Status == SD.RequestStatusPending && requestIdList.Contains(t.Id))
                .OrderByDescending(x => x.GenerateDate)
                .Select(x => new RequestViewModel
                {
                    Id = x.Id,
                    GenerateDate = x.GenerateDate,
                    Description = x.Description,
                    ConstructType = x.ConstructType,
                    Acreage = x.Acreage,
                    Location = x.Location,
                    Status = x.Status,
                    CusName = x.Customer.Name,
                    CusPhone = x.Customer.PhoneNumber,
                    CusEmail = x.Customer.Email,
                    CusGender = x.Customer.Gender
                })
                .ToList();

            //Return Json for datatables to read
            return Json(new { data = RequestVMlList });
        }

        /// <summary>
        /// This function get all Customer's Request completed in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRequestCompleted()
        {
			var requestIdList = GetWaitingRequestIdList();
			List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")

				.Where(t => t.Status == SD.RequestStatusSent && requestIdList.Contains(t.Id))
                .OrderByDescending(x => x.GenerateDate)
                .Select(x => new RequestViewModel
                {
                    Id = x.Id,
                    GenerateDate = x.GenerateDate,
                    Description = x.Description,
                    ConstructType = x.ConstructType,
                    Acreage = x.Acreage,
                    Location = x.Location,
                    Status = x.Status,
                    CusName = x.Customer.Name,
                    CusPhone = x.Customer.PhoneNumber,
                    CusEmail = x.Customer.Email,
                    CusGender = x.Customer.Gender
                })
                .ToList();

            //Return Json for datatables to read
            return Json(new { data = RequestVMlList });
        }

        /// <summary>
        /// This function get all Customer's Request rejected in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRequestRejected()
        {
			var requestIdList = GetWaitingRequestIdList();
			List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .Where(t => t.Status == SD.RequestStatusRejected && requestIdList.Contains(t.Id))
                .OrderByDescending(x => x.GenerateDate)
                .Select(x => new RequestViewModel
                {
                    Id = x.Id,
                    GenerateDate = x.GenerateDate,
                    Description = x.Description,
                    ConstructType = x.ConstructType,
                    Acreage = x.Acreage,
                    Location = x.Location,
                    Status = x.Status,
                    CusName = x.Customer.Name,
                    CusPhone = x.Customer.PhoneNumber,
                    CusEmail = x.Customer.Email,
                    CusGender = x.Customer.Gender
                })
                .ToList();

            //Return Json for datatables to read
            return Json(new { data = RequestVMlList });
        }

		[HttpGet]
		public async Task<IActionResult> GetAllRequestSaved()
		{
            var requestIdList = GetWaitingRequestIdList();
			List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
				.GetAll(includeProperties: "Customer")
				.OrderByDescending(x => x.GenerateDate)
				.Where(t => t.Status == SD.RequestStatusSaved && requestIdList.Contains(t.Id))
				.Select(x => new RequestViewModel
				{
					Id = x.Id,
					GenerateDate = x.GenerateDate,
					Description = x.Description,
					ConstructType = x.ConstructType,
					Acreage = x.Acreage,
					Location = x.Location,
					Status = x.Status,
					CusName = x.Customer.Name,
					CusPhone = x.Customer.PhoneNumber,
					CusEmail = x.Customer.Email,
					CusGender = x.Customer.Gender
				})
				.ToList();

			//Return Json for datatables to read
			return Json(new { data = RequestVMlList });
		}
		#endregion ============ API ============


		#region ============ ACTIONS ============
		/// <summary>
		/// This function change status of customer's request from "đang xử lí" to "từ chối báo giá"
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> RequestReject(string id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(x => x.Id == id);
            var quotation = _unitOfWork.CustomQuotation.Get(x => x.RequestId == id);
            if (requestForm == null)
            {
                TempData["error"] = "Từ chối báo giá thất bại";
            }
            else
            {
                requestForm.Status = SD.RequestStatusRejected;
                _unitOfWork.RequestForm.Update(requestForm);
                _unitOfWork.Save();
                if (quotation != null) 
                // quotation khác null nghĩa là đã có báo giá nên phải xóa và trả về trang hiện tại là Saved
                {
                    _unitOfWork.CustomQuotation.Remove(quotation);
                    _unitOfWork.Save();
                    TempData["success"] = "Từ chối báo giá thành công";
                    return RedirectToAction(nameof(ViewRequestSaved));
                }

                TempData["success"] = "Từ chối báo giá thành công";
                //quotation == null nghĩa là chưa có báo giá nên trả về trang hiện tại là Index
                
            }
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> UndoRejectRequest(string id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(x => x.Id == id);
            if(requestForm.Status == SD.RequestStatusRejected)
            {
                requestForm.Status = SD.RequestStatusPending;
                _unitOfWork.RequestForm.Update(requestForm);
                _unitOfWork.Save();
                TempData["success"] = "Hoàn tác yêu cầu thành công";
            }
            else
            {
                TempData["error"] = "Hoàn tác yêu cầu thất bại";
            }
           
           
            return RedirectToAction("ViewRequestRejected", "Request");
        }

        public async Task<IActionResult> ViewConstructDetails(string id)
        {
            var request = _unitOfWork.RequestForm.Get(x => x.Id == id, includeProperties: "CustomQuotation");
            ConstructDetailViewModel constructDetailVM = new ConstructDetailViewModel()
            {
                Alleys = SD.Alleys.Select(x => new SelectListItem() { Text = x, Value = x }).ToList(),
                Facades = SD.Facades.Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() }).ToList(),
                Basement = _unitOfWork.BasementType.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }).ToList(),
                Investment = _unitOfWork.InvestmentType.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }).ToList(),
                Construction = _unitOfWork.ConstructionType.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }).ToList(),
                Foundation = _unitOfWork.FoundationType.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }).ToList(),
                Rooftop = _unitOfWork.RoofType.GetAll().Select(x => new SelectListItem() { Text = x.Name, Value = x.Id }).ToList(),
                ConstructDetail = _unitOfWork.ConstructDetail.Get(x => x.QuotationId == request.CustomQuotation.Id, includeProperties: "Quotation")
            };
            return View(constructDetailVM);
        }

        public async Task<IActionResult> SendQuotation(string id)
        {
            var quotation = _unitOfWork.CustomQuotation.Get(x => x.Id == id);
            var request = _unitOfWork.RequestForm.Get(x => x.Id == quotation.RequestId);
            if (quotation != null && request != null)
            {
                quotation.Status = SD.Processing;
                request.Status = SD.RequestStatusSent;
                _unitOfWork.CustomQuotation.Update(quotation);
                _unitOfWork.RequestForm.Update(request);
                _unitOfWork.Save();
                TempData["success"] = "Gửi báo giá thành công";
            } else
            {
                TempData["error"] = "Gửi báo giá thất bại";
            }
            return RedirectToAction(nameof(ViewRequestSaved));
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> ViewRequestSaved()
        {
            return View();
        }

        public async Task<IActionResult> ViewRequestCompleted()
        {
            return View();
        }

        public async Task<IActionResult> ViewRequestRejected()
        {
            return View();
        }

        

        #endregion ============ ACTIONS ============


        #region ============ FUNCTIONS ============
        public List<string> GetWaitingRequestIdList()
        {
            var userId = SD.GetCurrentUserId(User);
            return _unitOfWork.WorkingReport.GetAll(x => x.StaffId == userId).Select(x => x.RequestId).ToList();
        }
        #endregion ============ FUNCTIONS ============
    }
}
