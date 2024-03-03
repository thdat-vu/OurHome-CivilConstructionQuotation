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
    public class ProjectController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        //ctor
        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        
        //TODO: Add a project datatable.js and call an api to retrieve the table
        public IActionResult Index()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAll().ToList();
            //return View();
            return View(objProjectList); //redirect to Index.cshtml + objList
        }

        //TODO: CREATE ProjectVM + fix that 
        public IActionResult Create()
        {
			//add ProjectVM to pass Properties.
			ProjectViewModel projectVM = new()
			{
				CustomerList = _unitOfWork.Customer.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Project = new Project()
			};
            return View(projectVM);
		}

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create (MaterialViewModel obj)
        {
            
                obj.Material.Status = true; //change status into true;
                _unitOfWork.Material.Add(obj.Material); //Add MaterialVM to Material table
                _unitOfWork.Save(); //keep track on change
				TempData["success"] = "Product created successfully";
				return RedirectToAction("Index"); //after adding, return to previous action and reload the page
            
            //return View(obj); //return previous action + invalid object
        }
        //Edit Action
        public IActionResult Edit (string? id)
        {
            if(id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1:retrieve Material from DB
            Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id);

            //catch not found exception
            if(materialFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
			//step 2: pass the Material obj Properties to MaterialViewModel instance
			//step 2.1: create MaterialViewModel
            MaterialViewModel materialVM = new()
            {
                CategoryList = _unitOfWork.MaterialCategory.GetAll().Select(u => new SelectListItem
                {
                    Text= u.Name,
                    Value = u.Id.ToString()
                }),
                Material = new Material()
            };
			//step 2.1: pass Material to MaterialViewModel
			materialVM.Material= materialFromDb;
			return View(materialVM); //return View + retrieved Material

            
        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(MaterialViewModel obj)
        {


           // if (ModelState.IsValid) //model is valid
            //{
                _unitOfWork.Material.Update(obj.Material); //Update Material to Material table
                _unitOfWork.Save(); //keep track on change
                TempData["success"] = "Material edited successfully";
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

        //    //retrieve Material from DB
        //    Material? materialFromDb = _unitOfWork.Material.Get(u => u.Id == id);

        //    if( materialFromDb == null)
        //    {
        //        return NotFound(); //return status not found aka 404
        //    }
                
        //    return View(materialFromDb); //return Delete.cshtml + materialFromDb


        //}
        ////delete request HttpPost, actionname=Delete
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(string? id)
        //{
        //    //retrieve Material from Db
        //    Material? obj = _unitOfWork.Material.Get(u => u.Id == id);
        //    //handle id null exception
        //    if (obj == null)
        //    {
        //        return NotFound();//return status not found aka 404
        //    }
        
        //    _unitOfWork.Material.Remove(obj); //just temporary
        //    _unitOfWork.Save();//keep track on change
        //    //TempData["success"] = "Product deleted successfully";
        //    return RedirectToAction("Index"); //redirect to Index.cshtml
        //}
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
			List<Project> objProjectList = _unitOfWork.Project.GetAll(includeProperties:"Customer").ToList();
			return Json(new { data = objProjectList }); //json + project list for data table.
		}
		#endregion
	}

}
