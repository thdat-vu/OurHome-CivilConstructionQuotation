using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using System.Net.NetworkInformation;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area(SD.Role_Manager)]
    [Authorize(Roles = SD.Role_Manager)]
    public class MaterialController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //inject IWebHostEnvironment to access wwwroot folder
        //ctor
        public MaterialController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }




        public IActionResult Index()
        {
            List<Material> objMaterialList = _unitOfWork.Material.GetAll().ToList();
            //return View();
            return View(objMaterialList); //redirect to Index.cshtml + objList
        }


        public IActionResult Create()
        {
            //add MaterialViewModel to pass Properties.
            MaterialViewModel materialVM = new()
            {
                CategoryList = _unitOfWork.MaterialCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Material = new Material()
            };
            return View(materialVM);
        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create(MaterialViewModel materialVM, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            materialVM.Material.Status = true; //change status into true;
            materialVM.Material.Id = SD.TempId;
            //handle the file
            //step1: check if the file name null or not
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                //product image location
                string materialPath = Path.Combine(wwwRootPath, @"images\material");
                //copy file's content to fileStream
                using (var fileStream = new FileStream(Path.Combine(materialPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                //pass the filename path to ImageUrl Property
                materialVM.Material.ImageUrl = @"\images\material\" + fileName;
            }
            _unitOfWork.Material.Add(materialVM.Material); //Add MaterialVM to Material table
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Material created successfully";
            return RedirectToAction("Index"); //after adding, return to previous action and reload the page

            //return View(materialVM); //return previous action + invalid object
        }
        //Edit Action
        public IActionResult Edit(string? id)
        {
            if (id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1:retrieve Material from DB
            Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id);

            //catch not found exception
            if (materialFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            //step 2: pass the Material materialVM Properties to MaterialViewModel instance
            //step 2.1: create MaterialViewModel
            MaterialViewModel materialVM = new()
            {
                CategoryList = _unitOfWork.MaterialCategory.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Material = new Material()
            };
            //step 2.1: pass Material to MaterialViewModel
            materialVM.Material = materialFromDb;
            return View(materialVM); //return View + retrieved Material


        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(MaterialViewModel materialVM, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                //product image location
                string materialPath = Path.Combine(wwwRootPath, @"images\material");

                if(!string.IsNullOrEmpty(materialVM.Material.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, materialVM.Material.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                //copy file's content to fileStream
                using (var fileStream = new FileStream(Path.Combine(materialPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                //pass the filename path to ImageUrl Property
                materialVM.Material.ImageUrl = @"\images\material\" + fileName;
            }
            // if (ModelState.IsValid) //model is valid
            //{
            _unitOfWork.Material.Update(materialVM.Material); //Update Material to Material table
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Material edited successfully";
            return RedirectToAction("Index"); //after updating, return to previous action and reload the page
            //}
            //return View();//return previous action if model is invalid
        }

        //Delete Action
        public IActionResult Delete(string? id)
        {
            //catch null id exception
            if (id == null)
            {
                return NotFound(); //return status not found aka 404
            }

            //retrieve Material from DB
            Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id, includeProperties: "Category");

            if (materialFromDb == null)
            {
                return NotFound(); //return status not found aka 404
            }

            return View(materialFromDb); //return Delete.cshtml + materialFromDb


        }
        //delete request HttpPost, actionname=Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            //retrieve Material from Db
            Material? obj = _unitOfWork.Material.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }

            //change the status in to false
            obj.Status = false;
            _unitOfWork.Save();//keep track on change
            TempData["success"] = "Material deleted successfully";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

        //get detail HttpGet
        [HttpGet]
        [ActionName("Detail")]
        public IActionResult GetDetail([FromQuery] string id)
        {
            var materialDetail = _unitOfWork.Material.Get((x) => x.Id == id);
            var materialDetailVM = new MaterialDetailViewModel()
            {
                Id = materialDetail.Id,
                MaterialName = materialDetail.Name,
                UnitPrice = materialDetail.UnitPrice,
                Unit = materialDetail.Unit,
                Status = materialDetail.Status,
                CategoryName = _unitOfWork.MaterialCategory.GetName(materialDetail.CategoryId)
            };
            //TODO: Test result
            return Json(new { data = materialDetailVM });
        }
        #region API CALLS
        /// <summary>
        /// This method return Json of Material List so that Database of Material can read.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Material> objMaterialList = _unitOfWork.Material.GetAllWithFilter(filter: m => m.Status == true, includeProperties: "Category").ToList();
            return Json(new { data = objMaterialList }); //json + material list for data table.

        }


        #endregion
    }

}
