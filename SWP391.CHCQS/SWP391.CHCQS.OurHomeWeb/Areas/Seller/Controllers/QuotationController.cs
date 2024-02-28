using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using SWP391.CHCQS.Utility;
using System.Text.Json;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Seller.Controllers
{
    [Area("Seller")]
    public class QuotationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuotationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        [HttpGet]
        public IActionResult CreateConstructDetails(string? id)
        {
            var requestForm = _unitOfWork.RequestForm.Get(u => u.Id == id);
            //phải có thằng này vì nó có khóa chung giữa 2 bảng là ConstructDetail và CustomQuotation, nếu muốn thêm thì phải thêm vào cả 2
            //hiểu nôm na là phải có CustomQuotation id trước khi có thằng ConstructDetail id
            var customQuotation = JsonSerializer.Serialize(new CustomQuotation
            {
                Id = "temp",
                Date = DateTime.Now,
                Acreage = requestForm.Acreage,
                Location = requestForm.Location,
                Status = SD.Preparing,
                Description = requestForm.Description,
                Total = 0,

            });
            ViewBag.CustomQuotation = customQuotation;

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
                ConstructDetail = new ConstructDetail(), 
                
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
                    QuotationId = "temp",
                  
                };

                // Thêm ConstructDetail vào unit of work và lưu thay đổi
                _unitOfWork.CustomQuotation.Add(customQuotation);
                _unitOfWork.ConstructDetail.Add(constructDetail);
                _unitOfWork.Save();

                TempData["success"] = "Construct Detail created successfully";
                return RedirectToAction("Index", "Home");
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
                return View(ConstructDetailVM);
            }

        }
        #region
        /// <summary>
        /// This function get all Quotation in Database and return it into JSON, this function ne lib Datatables to show data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		public async Task<IActionResult> GetAllQuotation()
		{
			List<CustomQuotation> CustomQuotationList = _unitOfWork.CustomQuotation
				.GetAll()
				.ToList();

			return Json(new { data = CustomQuotationList });
		}
        #endregion

        //#region
        ///// <summary>
        ///// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
        ///// </summary>
        ///// <returns></returns>
        ////[HttpGet]
        ////public async Task<IActionResult> GetAllConstructDetail()
        ////{
        ////	List<QuotationViewModel> QuotationVM = _unitOfWork.ConstructDetail
        ////		.GetAll(includeProperties: "CustomQuotation")
        ////		.Where(t => t != null)
        ////		.Select(x => new QuotationViewModel
        ////		{
        ////			Id = x.Id,
        ////			Date = x.Date,
        ////			Description = x.Description,
        ////			ConstructType = x.ConstructType,
        ////			Acreage = x.Acreage,
        ////			Location = x.Location,
        ////			Alley = x.Alley,
        ////			Balcony = x.Balcony,
        ////                  BasementId = x.BasementId,
        ////                  ConstructionId = x.ConstructionId,
        ////                  Facade = x.Facade,
        ////                  Floor = x.Floor,
        ////                  FoundationId = x.FoundationId,
        ////                  Garden = x.Garden,
        ////                  InvestmentId = x.InvestmentId,
        ////                  Length = x.Length,
        ////                  Mezzanine = x.Mezzanine,
        ////                  RooftopFloor = x.RooftopFloor,
        ////                  RooftopId = x.RooftopId,
        ////                  Room = x.Room,
        ////                  Width = x.Width
        ////		})
        ////		.ToList();

        ////	return Json(new { data = QuotationVM });
        ////}
        //#endregion

        public IActionResult ViewQuotation()
        {
            return View();
        }
		public IActionResult Details()
        {
            return View();
        }
    }
}
