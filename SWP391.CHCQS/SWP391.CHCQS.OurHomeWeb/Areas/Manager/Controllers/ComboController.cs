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
	[Area("Manager")]
	public class ComboController : Controller
	{
		//init IUnitOfWork
		private readonly IUnitOfWork _unitOfWork;

		//ctor
		public ComboController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
			ComboDetailViewModel comboDetailViewModel = new ComboDetailViewModel();
			return View(comboDetailViewModel);

		}

		//Create request HttpPOST
		[HttpPost]
		public IActionResult Create(StandardQuotationViewModel obj)
		{
			//if (ModelState.IsValid) //if obj is valid
			//{
			obj.StandardQuotation.Status = true;
			obj.StandardQuotation.Id = SD.TempId;
			_unitOfWork.Combo.Add(obj.StandardQuotation); //Add Combo to Combo table
			_unitOfWork.Save(); //keep track on change
			TempData["success"] = "Combo added successfully";
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
			//step 1: retrieve Combo from DB
			Combo? standardQuotationFromDb = _unitOfWork.Combo.Get(u => u.Id == id);

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
				StandardQuotation = new Combo()
			};
			//step 2.1: pass Material to MaterialViewModel
			standardQuotationVM.StandardQuotation = standardQuotationFromDb;

			return View(standardQuotationVM); //return View + retrieved Combo

		}
		//Edit request with HttpPOST method
		[HttpPost]
		public IActionResult Edit(StandardQuotationViewModel obj)
		{


			//if (ModelState.IsValid) //model is valid
			//{
				_unitOfWork.Combo.Update(obj.StandardQuotation); //Update Combo to Combo table
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
							   //TempData["success"] = "Product deleted successfully";
			return RedirectToAction("Index"); //redirect to Index.cshtml
		}

		#region API CALL
		public IActionResult GetAll()
		{
			List<Combo> objComboList = _unitOfWork.Combo.GetAllWithFilter(filter: cb => cb.Status == true,includeProperties: "Construction").ToList();

			return Json(new { data = objComboList });
		}
		#endregion
	}
}
