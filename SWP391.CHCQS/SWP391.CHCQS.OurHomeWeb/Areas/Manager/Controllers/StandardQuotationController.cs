using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
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
			StandardQuotationViewModel standardQuotationVM = new StandardQuotationViewModel()
			{
				ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u
				=> new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString(),
				}),
				StandardQuotation = new StandardQuotation()
			};
			return View(standardQuotationVM);

		}

		//Create request HttpPOST
		[HttpPost]
		public IActionResult Create(StandardQuotationViewModel obj)
		{
			//if (ModelState.IsValid) //if obj is valid
			//{
			obj.StandardQuotation.Status = true;
			_unitOfWork.StandardQuotation.Add(obj.StandardQuotation); //Add StandardQuotation to StandardQuotation table
			_unitOfWork.Save(); //keep track on change
			TempData["success"] = "StandardQuotation added successfully";
			return RedirectToAction("Index"); //after adding, return to previous action and reload the page
											  //}
											  //return View(obj); //return previous action + invalid object
		}
		//Edit Action
		public IActionResult Edit(string? id)
		{
			if (id == null) //id is null
			{
				return NotFound();//return status not found aka 404
			}
			//step 1: retrieve StandardQuotation from DB
			StandardQuotation? standardQuotationFromDb = _unitOfWork.StandardQuotation.Get(u => u.Id == id);

			//catch not found exception
			if (standardQuotationFromDb == null)
			{
				return NotFound();//return status not found aka 404
			}
			//step 2: pass the standardQuotationFromDb obj Properties to standardQuotationVM instance
			//step 2.1: create standardQuotationVM
			StandardQuotationViewModel standardQuotationVM = new()
			{
				ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				StandardQuotation = new StandardQuotation()
			};
			//step 2.1: pass Material to MaterialViewModel
			standardQuotationVM.StandardQuotation = standardQuotationFromDb;

			return View(standardQuotationVM); //return View + retrieved StandardQuotation

		}
		//Edit request with HttpPOST method
		[HttpPost]
		public IActionResult Edit(StandardQuotationViewModel obj)
		{


			//if (ModelState.IsValid) //model is valid
			//{
				_unitOfWork.StandardQuotation.Update(obj.StandardQuotation); //Update StandardQuotation to StandardQuotation table
				_unitOfWork.Save(); //keep track on change
				TempData["success"] = "StandardQuotation edited successfully";
				return RedirectToAction("Index"); //after updating, return to previous action and reload the page
			//}
			//return View();//return previous action if model is invalid
		}

		//Delete Action
		//public IActionResult Delete(string? id)
		//{
		//    //catch null id exception
		//    if (id == null)
		//    {
		//        return NotFound(); //return status not found aka 404
		//    }

		//    //retrieve StandardQuotation from DB
		//    StandardQuotation? materialFromDb = _unitOfWork.StandardQuotation.Get(u => u.Id == id);

		//    if( materialFromDb == null)
		//    {
		//        return NotFound(); //return status not found aka 404
		//    }

		//    return View(materialFromDb); //return Delete.cshtml + materialFromDb


		//}
		//delete request HttpPost, actionname=Delete
		//[HttpPost, ActionName("Delete")]
		//public IActionResult DeletePOST(string? id)
		//{
		//    //retrieve StandardQuotation from Db
		//    StandardQuotation? obj = _unitOfWork.StandardQuotation.Get(u => u.Id == id);
		//    //handle id null exception
		//    if (obj == null)
		//    {
		//        return NotFound();//return status not found aka 404
		//    }

		//    _unitOfWork.StandardQuotation.Remove(obj); //just temporary
		//    _unitOfWork.Save();//keep track on change
		//    //TempData["success"] = "Product deleted successfully";
		//    return RedirectToAction("Index"); //redirect to Index.cshtml
		//}

		#region API CALL
		public IActionResult GetAll()
		{
			List<StandardQuotation> objStandardQuotationList = _unitOfWork.StandardQuotation.GetAll(includeProperties: "Construction").ToList();

			return Json(new { data = objStandardQuotationList });
		}
		#endregion
	}
}
