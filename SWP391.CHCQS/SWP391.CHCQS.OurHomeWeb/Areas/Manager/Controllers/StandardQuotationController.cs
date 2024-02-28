using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System.Net.NetworkInformation;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area("Manager")]
    public class StandardQuotationController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public StandardQuotationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //getAll() action
        public IActionResult Index()
        {
            List<StandardQuotation> objStandardQuotationList = _unitOfWork.StandardQuotation.GetAll().ToList();
            //return View();
            return View(objStandardQuotationList); //redirect to Index.cshtml + objList
        }

        //Create Action
        public IActionResult Create()
        {
            //redirect to Create.cshtml
            return View();

        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create (StandardQuotation obj)
        {
            if (ModelState.IsValid) //if obj is valid
            {
                _unitOfWork.StandardQuotation.Add(obj); //Add StandardQuotation to StandardQuotation table
                _unitOfWork.Save(); //keep track on change
                return RedirectToAction("Index"); //after adding, return to previous action and reload the page
            }
            return View(obj); //return previous action + invalid object
        }
        //Edit Action
        public IActionResult Edit (string? id)
        {
            if(id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //retrieve StandardQuotation from DB
            StandardQuotation? materialFromDb = _unitOfWork.StandardQuotation.Get(u => u.Id == id);

            //catch not found exception
            if(materialFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            return View(materialFromDb); //return View + retrieved StandardQuotation

            
        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(StandardQuotation obj)
        {


            if (ModelState.IsValid) //model is valid
            {
                _unitOfWork.StandardQuotation.Update(obj); //Update StandardQuotation to StandardQuotation table
                _unitOfWork.Save(); //keep track on change
               //TempData["success"] = "StandardQuotation edited successfully";
                return RedirectToAction("Index"); //after updating, return to previous action and reload the page
            }
            return View();//return previous action if model is invalid
        }

        //Delete Action
        public IActionResult Delete(string? id)
        {
            //catch null id exception
            if (id == null)
            {
                return NotFound(); //return status not found aka 404
            }

            //retrieve StandardQuotation from DB
            StandardQuotation? materialFromDb = _unitOfWork.StandardQuotation.Get(u => u.Id == id);

            if( materialFromDb == null)
            {
                return NotFound(); //return status not found aka 404
            }
                
            return View(materialFromDb); //return Delete.cshtml + materialFromDb


        }
        //delete request HttpPost, actionname=Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            //retrieve StandardQuotation from Db
            StandardQuotation? obj = _unitOfWork.StandardQuotation.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }
        
            _unitOfWork.StandardQuotation.Remove(obj); //just temporary
            _unitOfWork.Save();//keep track on change
            //TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

		#region API CALL
        public IActionResult GetAll()
        {
			List<StandardQuotation> objStandardQuotationList = _unitOfWork.StandardQuotation.GetAll().ToList();
			
			return Json( new { data = objStandardQuotationList });
		}
		#endregion
	}
}
