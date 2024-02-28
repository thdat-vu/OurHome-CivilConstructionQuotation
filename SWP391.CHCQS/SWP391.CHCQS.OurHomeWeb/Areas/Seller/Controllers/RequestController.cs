    using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class RequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region
        /// <summary>
        /// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RequestViewModel> RequestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .Where(t => t.Status == true)
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
                    CusPhone = x.Customer.PhoneNum,
                    CusEmail = x.Customer.Email,
                    CusGender = x.Customer.Gender
                })
                .ToList();

            return Json(new { data = RequestVMlList });
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
