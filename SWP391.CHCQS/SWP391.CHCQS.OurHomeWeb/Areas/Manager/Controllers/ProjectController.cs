using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using System.Drawing;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area(SD.Role_Manager)]
    [Authorize(Roles = SD.Role_Manager)]
    public class ProjectController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        
        //ctor
        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }




        public IActionResult Index()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAll().ToList();
            //return View();
            return View(objProjectList); //redirect to Index.cshtml + objList
        }


        public IActionResult Create()
        {
            //add ProjectViewModel to pass Properties.
            ProjectVM productVM = new();
            return View(productVM);
        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create(ProjectVM projectVM)
        {
            
            projectVM.Status = true; //change status into true;
            projectVM.Id = SD.TempId;
            DateTime parsedDate;
            if (DateTime.TryParseExact(Request.Form["Date"], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                projectVM.Date = parsedDate;
            }
            else
            {
                ModelState.AddModelError("Date", "Sai định dạng ngày tháng");
            }


            Project project = new Project()
            {
                Id = projectVM.Id,
                Name = projectVM.Name,
                Location = projectVM.Location,
                Scale = projectVM.Scale,
                Size = projectVM.Size,
                Status = projectVM.Status,
                Description = projectVM.Description,
                Overview = projectVM.Overview,
                Date = projectVM.Date,
                CustomerId = projectVM.CustomerId

            };

            
            _unitOfWork.Project.Add(project); //Add ProjectVM to Project table
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Thêm dự án thành công";
            return RedirectToAction("Index"); //after adding, return to previous action and reload the page

            //return View(projectVM); //return previous action + invalid object
        }
        //Edit Action
        public IActionResult Edit(string? id)
        {
            if (id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1:retrieve Project from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == id, includeProperties: "Customer");

            //catch not found exception
            if (projectFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            //step 2: pass the Material projectVM Properties to MaterialViewModel instance
            //step 2.1: create MaterialViewModel
            ProjectVM projectVM = new()
            {
                Id = projectFromDb.Id,
                Name = projectFromDb.Name,
                Location = projectFromDb.Location,
                Scale = projectFromDb.Scale,
                Size = projectFromDb.Size,
                Status = projectFromDb.Status,
                Description = projectFromDb.Description,
                Overview = projectFromDb.Overview,
                Date = projectFromDb.Date,
                CustomerId = projectFromDb.CustomerId,
                CustomerName = projectFromDb.Customer.Name
            };
            //step 2.1: pass ProjectVM to ProjectVM
            //projectVM.Material = materialFromDb;
            return View(projectVM); //return View + retrieved 
        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(ProjectVM obj)
        {


            // if (ModelState.IsValid) //model is valid
            //{
            //step 1:retrieve Project from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == obj.Id, includeProperties: "Customer");
            //catch not found exception
            if (projectFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            projectFromDb.Name = obj.Name;
            projectFromDb.Location = obj.Location;
            projectFromDb.Scale = obj.Scale;
            projectFromDb.Size = obj.Size;
            projectFromDb.Description = obj.Description;
            projectFromDb.Overview = obj.Overview;
            projectFromDb.Date = obj.Date;
            projectFromDb.CustomerId = obj.CustomerId;
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Chỉnh sửa dự án thành công";
            return RedirectToAction("Index"); //after updating, return to previous action and reload the page
                                              //    //}
                                              //    //return View();//return previous action if model is invalid
        }

        //Delete Action
        public IActionResult Delete(string? id)
        {
            //catch null id exception
            if (id == null)
            {
                return NotFound(); //return status not found aka 404
            }

            //retrieve projectFromDb from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == id, includeProperties: "Customer");

            if (projectFromDb == null)
            {
                return NotFound(); //return status not found aka 404
            }

            return View(projectFromDb); //return Delete.cshtml + projectFromDb


        }
        //delete request HttpPost, actionname=Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            //retrieve Material from Db
            Project? obj = _unitOfWork.Project.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }

            //change the status in to false
            obj.Status = false;
            _unitOfWork.Save();//keep track on change
            TempData["success"] = "Xóa dự án thành công";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

       
        #region API CALLS
        /// <summary>
        /// this method return a Json Of Project List so that Project DataTable can read.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAllWithFilter(filter: p => p.Status == true, includeProperties: "Customer").ToList();
            return Json(new { data = objProjectList }); //json + material list for data table.
        }


        #endregion
    }

}
