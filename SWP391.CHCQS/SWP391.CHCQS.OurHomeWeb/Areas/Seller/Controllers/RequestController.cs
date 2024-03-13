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
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .Where(t => t.Status == SD.RequestStatusPending)
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
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")

				.Where(t => t.Status == SD.RequestStatusSent)
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
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .Where(t => t.Status == SD.RequestStatusRejected)
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
			List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
				.GetAll(includeProperties: "Customer")
				.OrderBy(x => x.GenerateDate)
				.Where(t => t.Status == SD.RequestStatusSaved)
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
            requestForm.Status = SD.RequestStatusRejected;
            _unitOfWork.RequestForm.Update(requestForm);
            _unitOfWork.CustomQuotation.Remove(quotation);
            _unitOfWork.Save();
            TempData["success"] = "Từ chối báo giá thành công";


            return RedirectToAction(nameof(ViewRequestRejected));
        }

       
        public async Task<IActionResult> UndoRejectRequest(string id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(x => x.Id == id);
            if(requestForm.Status == SD.RequestStatusRejected)
            {
                requestForm.Status = SD.RequestStatusPending;
                _unitOfWork.RequestForm.Update(requestForm);
                _unitOfWork.Save();
                TempData["success"] = "Hoàn tác báo giá thành công";
            }
            else
            {
                TempData["error"] = "Hoàn tác báo giá thất bại";
            }
           
           
            return RedirectToAction("ViewRequestRejected", "Request");
        }

        public async Task<IActionResult> ViewConstructDetails(string id)
        {
            var request = _unitOfWork.RequestForm.Get(x => x.Id == id, includeProperties: "CustomQuotation");
            ConstructDetailViewModel constructDetailVM = new ConstructDetailViewModel()
            {
                Alleys = SD.Alleys.Select(x => new SelectListItem() { Text = x, Value = x }).ToList(),
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
            quotation.Status = SD.Processing;
            request.Status = SD.RequestStatusSent;
            _unitOfWork.CustomQuotation.Update(quotation);
            _unitOfWork.RequestForm.Update(request);
            _unitOfWork.Save();
            TempData["success"] = "Gửi báo giá thành công";
            return RedirectToAction("ViewQuotation", "Quotation");
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

        #endregion ============ FUNCTIONS ============
    }
}
