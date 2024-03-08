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
    public class StandardQuotationController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //inject IWebHostEnvironment to access wwwroot folder

        //ctor
        public StandardQuotationController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        //getAll() action
        public IActionResult Index()
        {
            List<Combo> objStandardQuotationList = _unitOfWork.Combo.GetAll().ToList();
            //return View();
            return View(objStandardQuotationList); //redirect to Index.cshtml + objList
        }

        //Create Action
        public IActionResult Create()
        {
            StandardQuotationViewModel standardQuotationVM = new StandardQuotationViewModel()
            {
                ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u
                => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                StandardQuotation = new Combo()
            };
            return View(standardQuotationVM);

        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create(StandardQuotationViewModel standardQuotationVM, IFormFile? file)
        {
            //if (ModelState.IsValid) //if standardQuotationVM is valid
            //{
            string wwwRootPath = _webHostEnvironment.WebRootPath; //retrieve webrootpath

            standardQuotationVM.StandardQuotation.Status = true;
            standardQuotationVM.StandardQuotation.Id = SD.TempId;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                //product image location
                string materialPath = Path.Combine(wwwRootPath, @"images\combo");
                //copy file's content to fileStream
                using (var fileStream = new FileStream(Path.Combine(materialPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                //pass the filename path to ImageUrl Property
                standardQuotationVM.StandardQuotation.ImageUrl = @"\images\combo\" + fileName;
            }
            _unitOfWork.Combo.Add(standardQuotationVM.StandardQuotation); //Add Combo to Combo table
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Combo added successfully";
            return RedirectToAction("Index"); //after adding, return to previous action and reload the page
                                              
                                              
        }
        //Edit Action
        public IActionResult Edit(string? id)
        {
            if (id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1: retrieve Combo from DB
            Combo? standardQuotationFromDb = _unitOfWork.Combo.Get(u => u.Id == id);

            //catch not found exception
            if (standardQuotationFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            //step 2: pass the standardQuotationFromDb standardQuotationVM Properties to standardQuotationVM instance
            //step 2.1: create standardQuotationVM
            StandardQuotationViewModel standardQuotationVM = new()
            {
                ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                StandardQuotation = new Combo()
            };
            //step 2.1: pass Material to MaterialViewModel
            standardQuotationVM.StandardQuotation = standardQuotationFromDb;

            return View(standardQuotationVM); //return View + retrieved Combo

        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(StandardQuotationViewModel comboVM, IFormFile? file)
        {


            //if (ModelState.IsValid) //model is valid
            //{
           
            //retrieve rootpath
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            //File handle on Update 
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                //product image location
                string materialPath = Path.Combine(wwwRootPath, @"images\combo");

                if (!string.IsNullOrEmpty(comboVM.StandardQuotation.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath = Path.Combine(wwwRootPath, comboVM.StandardQuotation.ImageUrl.TrimStart('\\'));
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
                comboVM.StandardQuotation.ImageUrl = @"\images\combo\" + fileName;
            }
            _unitOfWork.Combo.Update(comboVM.StandardQuotation);//Update Combo to Combo table
            _unitOfWork.Save(); //keep track on change
            TempData["success"] = "Combo edited successfully";
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

            //retrieve Combo from DB
            Combo? materialFromDb = _unitOfWork.Combo.Get(u => u.Id == id, includeProperties: "Construction");

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
            //retrieve Combo from Db
            Combo? obj = _unitOfWork.Combo.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }

            //change the status in to false
            obj.Status = false;
            _unitOfWork.Save();//keep track on change
            TempData["success"] = "Combo deleted successfully";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

        #region API CALL
        /// <summary>
        /// This method return the JSON of a list that Datatable of the Combo can read.
        /// </summary>
        /// <returns>Json(new { data = objStandardQuotationList });</returns>
        public IActionResult GetAll()
        {
            List<Combo> objStandardQuotationList = _unitOfWork.Combo.GetAllWithFilter(filter: sq => sq.Status == true, includeProperties: "Construction").ToList();

            return Json(new { data = objStandardQuotationList });
        }
        #endregion
    }
}
