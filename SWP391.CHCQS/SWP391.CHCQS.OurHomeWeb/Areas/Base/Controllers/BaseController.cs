﻿using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.Models;
using SWP391.CHCQS.OurHomeWeb.Models;
using SWP391.CHCQS.Utility.Helpers;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.OurHomeWeb.Areas.Seller.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SWP391.CHCQS.Model;
using System.Xml.Linq;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Base.Controllers
{
    [Area("Base")]
    [Authorize(Policy = "BasePolicy")]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IWebHostEnvironment _environment;
        protected readonly UserManager<IdentityUser>? _userManager;
        public BaseController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, UserManager<IdentityUser>? userManager = null)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _userManager = userManager;
        }
        //public BaseController(IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        //{
        //    _unitOfWork = unitOfWork;
        //    _environment = environment;
        //}
        /// <summary>
        /// Hàm lấy dữ liệu của note từ File và Session để đưa lên cho người dùng
        /// Trình tự hoạt động như sau:
        /// ---     KHÔNG CÓ NOTE TRONG SESSION     ---
        ///     1. Nạp các taskdetail và material detail thành key tương đương vào biến 'rejectDetail' của class RejectQuotationDetail với note và value rỗng 
        ///         2. Kiểm tra file 
        ///                 2.1-KHÔNG CÓ NOTE FILE. Không làm gì cả
        ///                 2.2-CÓ NOTE FILE. kiểm tra nếu trong session có key tương đương với note thì tiến hành cập nhật note cho key từ file lên
        ///             3. Cập nhật lại vào Session
        /// Trả về đối tượng chứa note lấy ra từ session
        /// ---     CÓ NOTE TRONG SESSION       ----
        /// Trả về đối tượng chứa note lấy ra từ session
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public RejectQuotationDetail GetRejectQuotationDetailFromSessionAndFile()
        {
            //LẤY ID CỦA QUOTATION ĐAG DC XEM
            var quoteId = HttpContext.Session.GetString(SessionConst.QUOTATION_ID);
            // Kiểm tra xem trong session có note ko ???
            var rejectDetail = HttpContext.Session.Get<RejectQuotationDetail>(quoteId);
            //Nếu ko có thì tiến hành nạp các taskdetail và material detail với key là id của task và material cùng default value
            if (rejectDetail == null)
            {
                rejectDetail = new RejectQuotationDetail()
                {
                    MaterialDetailNotes = new Dictionary<string, MaterialNote>(),
                    TaskDetailNotes = new Dictionary<string, string>(),
                };
                //Tiến hành tạo các chỗ chứa note cho từng taskId và materialId dựa trên taskDetail và materialDetail
                //lấy ra các taskId thuộc về customquotation đó 
                var taskIdDetailList = _unitOfWork.TaskDetail.GetAllWithFilter((t) => t.QuotationId == quoteId).Select(t => t.TaskId).ToList();
                //đưa default value vào - place holder
                taskIdDetailList.ForEach(t =>
                {
                    rejectDetail.TaskDetailNotes.Add(t, "");
                });
                //lấy ra các materialId thuộc về customquotation đó 
                var materialIdDetailList = _unitOfWork.MaterialDetail.GetAllWithFilter((t) => t.QuotationId == quoteId).Select(t => t.MaterialId).ToList();
                //đưa default value vào - place holder
                materialIdDetailList.ForEach(m =>
                {
                    rejectDetail.MaterialDetailNotes.Add(m, new MaterialNote()
                    {
                        Quantity = 0,
                        Note = ""
                    });
                });

                //đọc từ file lên, so sánh với lần note cuối cùng, với mỗi key tương ứng mà trong file có thì cập nhật vào session
                var pathCreater = new PathCreater(_environment);
                string targetFolder = pathCreater.CreateFilePathInRoot(quoteId.Trim() + ".txt", "note-reject-quotation-file");
                List<RejectQuotationDetail> noteObj = FileManipulater<RejectQuotationDetail>.LoadJsonFromFile(targetFolder);
                //Phải có take note dc lưu từ lần trước vào file thì kiểm tra nội dung được
                if (noteObj.Count != 0)
                {
                    var note = noteObj.Last();
                    //kiểm tra note cho task
                    foreach (var key in rejectDetail.TaskDetailNotes.Keys)
                    {
                        //nếu trong note có chứa key thì mới kiểm tra nội dung
                        if (note.TaskDetailNotes.ContainsKey(key))
                            //nếu nội dung khác thì cập nhật lại lên trên
                            if (note.TaskDetailNotes[key] != rejectDetail.TaskDetailNotes[key])
                                rejectDetail.TaskDetailNotes[key] = note.TaskDetailNotes[key];
                    }

                    //kiểm tra note cho material
                    foreach (var key in rejectDetail.MaterialDetailNotes.Keys)
                    {
                        //nếu trong note có chứa key thì mới kiểm tra nội dung
                        if (note.MaterialDetailNotes.ContainsKey(key))
                            //nếu nội dung khác thì cập nhật lại lên trên
                            if (note.MaterialDetailNotes[key].Note != rejectDetail.MaterialDetailNotes[key].Note)
                                rejectDetail.MaterialDetailNotes[key].Note = note.MaterialDetailNotes[key].Note;
                    }
                }
            }
            //lưu trữ với Key tương ứng với Id của customquotation
            HttpContext.Session.Set<RejectQuotationDetail>(quoteId, rejectDetail);
            return rejectDetail;
        }
        /// <summary>
		/// This function get all Customer's Request in Database and return it into JSON, this function ne lib Datatables to show data
		/// </summary>
		/// <returns></returns>
		[HttpGet]
        public async Task<IActionResult> GetAllRequest()
        {
            
            
            List<RequestViewModel> requestVMlList = _unitOfWork.RequestForm
                .GetAll(includeProperties: "Customer")
                .OrderBy(x => x.GenerateDate)
                .Select(x => new RequestViewModel
                {
                    Id = x.Id,
                    GenerateDate = x.GenerateDate,
                    //Description = x.Description,
                    //ConstructType = x.ConstructType,
                    //Acreage = x.Acreage,
                   // Location = x.Location,
                    Status = x.Status,
                    CusName = x.Customer.Name,
                    CusPhone = x.Customer.PhoneNumber,
                    CusEmail = x.Customer.Email,
                    //CusGender = x.Customer.Gender
                })
                .ToList();
            requestVMlList.ForEach(x =>
            {
                //lấy working report của rf đó ra
                var workingReport = _unitOfWork.WorkingReport.GetAllWithFilter((x) => x.RequestId == x.RequestId);
                //xác nhận role của nhân viên đó
                foreach (var workReport in workingReport)
                {
                    //lấy nhân viên ra
                    var staff = _userManager.FindByIdAsync(workReport.StaffId).GetAwaiter().GetResult() as ApplicationUser;
                    //xác nhận role của nhân viên đó
                    var role =  _userManager.GetRolesAsync(staff).GetAwaiter().GetResult();
                    //gán cho biến name với staff role tương ứng
                    if (role.First() == SD.Role_Seller)
                        x.SellerName = staff.Name;
                    if (role.First() == SD.Role_Engineer)
                        x.EngineerName = staff.Name;
                    if (role.First() == SD.Role_Manager)
                        x.ManagerName = staff.Name;
                }
            });
            return Json(new { data = requestVMlList });
        }

        public void NotifySuccess(string notification)
        {
            TempData["Success"] = notification;
        }

        public void NotifyError(string notification)
        {
            TempData["Error"] = notification;
        }
    }
}
