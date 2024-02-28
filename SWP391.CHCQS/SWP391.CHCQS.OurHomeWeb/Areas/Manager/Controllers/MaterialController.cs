using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using System.Net.NetworkInformation;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area("Manager")]
    public class MaterialController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public MaterialController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        
        
        public IActionResult Index()
        {
            List<Material> objMaterialList = _unitOfWork.Material.GetAll().ToList();
            //return View();
            return View(objMaterialList); //redirect to Index.cshtml + objList
        }

        
        public IActionResult Create()
        {
            return View();
        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create (Material obj)
        {
            if (ModelState.IsValid) //if obj is valid
            {
                _unitOfWork.Material.Add(obj); //Add Material to Material table
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
            //retrieve Material from DB
            Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id);

            //catch not found exception
            if(materialFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            return View(materialFromDb); //return View + retrieved Material

            
        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(Material obj)
        {


            if (ModelState.IsValid) //model is valid
            {
                _unitOfWork.Material.Update(obj); //Update Material to Material table
                _unitOfWork.Save(); //keep track on change
               //TempData["success"] = "Material edited successfully";
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

            //retrieve Material from DB
            Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id);

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
            //retrieve Material from Db
            Material? obj = _unitOfWork.Material.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }
        
            _unitOfWork.Material.Remove(obj); //just temporary
            _unitOfWork.Save();//keep track on change
            //TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }
        //get detail HttpGet
        [HttpGet]
        [ActionName("Detail")]
        public IActionResult GetDetail([FromQuery]string id)
        {
            var materialDetail = _unitOfWork.Material.Get((x) => x.Id == id);
            var materialDetailVM = new MaterialDetailViewModel()
            {
                Id = materialDetail.Id,
                MaterialName = materialDetail.Name,
                InventoryQuantity = materialDetail.InventoryQuantity,
                UnitPrice = materialDetail.UnitPrice,
                Unit = materialDetail.Unit,
                Status = materialDetail.Status,
                CategoryName = _unitOfWork.MaterialCategory.GetName(materialDetail.CategoryId)
            };
            //TODO: Test result
            return Json(new {data = materialDetailVM});
        }
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
        {
			List<Material> objMaterialList = _unitOfWork.Material.GetAll(includeProperties:"Category").ToList();
			return Json(new { data = objMaterialList }); //json + material list for data table.
		}


		#endregion
	}

}
