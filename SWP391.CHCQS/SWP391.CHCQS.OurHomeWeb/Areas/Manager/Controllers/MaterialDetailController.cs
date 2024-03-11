using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Linq;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class MaterialDetailController : BaseController
    {


        public MaterialDetailController(IUnitOfWork unitOfWork, IWebHostEnvironment environment) : base(unitOfWork, environment)
        {

        }
        #region GỌI API
        /// <summary>
        /// Action để gọi API trả về material detail
        /// Các bước thực hiện:
        /// 1. lấy quoteId đã được lưu vào session, đã dc lưu ghi người dùng GetDetail của Customquotation
        /// 2. Lấy note dc lưu trong session và file
        /// 3. Tiến hành đưa cho ViewModel trả về Json
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDetail()
        {
            //thêm thông tin task detail
            string quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            //lấy note từ session
            var rejectDetail = GetRejectQuotationDetailFromSessionAndFile();

            var materialNote = rejectDetail.MaterialDetailNotes;
            //thêm thông tin material detail
            List<MaterialDetailListViewModel> materialDetailVM = _unitOfWork.MaterialDetail.GetMaterialDetail(quoteId, "Material")
                .Select((x) => new ViewModels.MaterialDetailListViewModel
                {
                    QuoteId = x.QuotationId,
                    MaterialId = x.MaterialId,
                    //MaterialName = _unitOfWork.Material.GetName(x.MaterialId),
                    MaterialName = x.Material.Name,
                    Unit = x.Material.Unit,
                    MaterialCateName = _unitOfWork.MaterialCategory.GetName(x.Material.CategoryId),
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Note = new KeyValuePair<string, MaterialNote>(x.MaterialId, new MaterialNote()
                    {
                        Quantity = x.Quantity,
                        Note = materialNote[x.MaterialId].Note,
                    })
                }).ToList();
            return Json(new { data = materialDetailVM });
        }
        #endregion
    }
}
