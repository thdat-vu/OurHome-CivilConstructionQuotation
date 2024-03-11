    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = SD.Role_Seller)]
    public class RequestController : Controller
    {
        #region ============ DECLARE ============
        private readonly IUnitOfWork _unitOfWork;
        public RequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion ============ DECLARE ============

        #region ============ ACTIONS ============
        public async Task<IActionResult> RejectRequest(int id)
        {

            return View(id);    
        }

		#endregion ============ ACTIONS ============

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
                .OrderBy(x => x.GenerateDate)
                .Where(t => t.Status == SD.RequestStatusPending)
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

            return Json(new { data = RequestVMlList });
        }

        /// <summary>
        /// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRequestCompleted()
        {
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
				.OrderBy(x => x.GenerateDate)
				.Where(t => t.Status == SD.RequestStatusApproved)
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

            return Json(new { data = RequestVMlList });
        }

        /// <summary>
        /// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRequestRejected()
        {
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .OrderBy(x => x.GenerateDate)
                .Where(t => t.Status == SD.RequestStatusRejected)
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

            return Json(new { data = RequestVMlList });
        }
        #endregion ============ API ============

        #region ============ ACTIONS ============
        public async Task<IActionResult> RequestReject(string id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(x => x.Id == id);
            if (requestForm.Status == SD.RequestStatusPending)
            {
                requestForm.Status = SD.RequestStatusRejected;
                _unitOfWork.RequestForm.Update(requestForm);
                _unitOfWork.Save();
                TempData["success"] = "Rejected successfully";
            }


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
                TempData["success"] = "Undo reject request successfully";
            }
           
           
            return RedirectToAction("ViewRequestRejected", "Request");
        }


        #endregion ============ ACTIONS ============

        #region ============ FUNCTIONS ============
        public async Task<IActionResult> Index()
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
        #endregion ============ FUNCTIONS ============
    }
}
