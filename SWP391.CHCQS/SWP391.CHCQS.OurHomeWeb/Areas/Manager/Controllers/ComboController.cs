using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Engineer.ViewModels;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using SWP391.CHCQS.Utility.Helpers;
using System.Net.NetworkInformation;
using MaterialViewModel = SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels.MaterialViewModel;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area("Manager")]
    [Authorize(Roles = SD.Role_Manager)]
    public class ComboController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //inject IWebHostEnvironment to access wwwroot folder
        //ctor
        public ComboController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        //getAll() action
        public IActionResult Index()
        {
            //Remove section after adding, editing, deleting
            HttpContext.Session.Remove(SessionConst.COMBO_TASK_LIST_KEY);
            HttpContext.Session.CommitAsync();
            HttpContext.Session.Remove(SessionConst.COMBO_MATERIAL_LIST_KEY);
            HttpContext.Session.CommitAsync();

            List<Combo> objStandardQuotationList = _unitOfWork.Combo.GetAll().ToList();
            //return View();
            return View(objStandardQuotationList); //redirect to Index.cshtml + objList
        }

        //Create Action
        public IActionResult Create()

        {
            //set up empty ComboDetailViewModel + dropdown list of ConstructionTypeList
            ComboDetailViewModel comboDetailViewModel = new ComboDetailViewModel()
            {
                ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u
                => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Combo = new Combo(),
                ComboMaterials = new List<ComboMaterial>(),
                ComboTasks = new List<Model.ComboTask>()
            };
            return View(comboDetailViewModel);

        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create(ComboDetailViewModel viewModel, IFormFile? file)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; //retrieve webrootpath
                viewModel.Combo.Status = true;
                viewModel.Combo.Id = SD.TempId;
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
                    viewModel.Combo.ImageUrl = @"\images\combo\" + fileName;
                }

                //try to get COMBO_MATERIAL_LIST session 
                var comboMaterialListSession = HttpContext.Session.Get<List<MaterialViewModel>>(SessionConst.COMBO_MATERIAL_LIST_KEY);

                //try to get Combo_Task_List session

                var comboTaskListSession = HttpContext.Session.Get<List<TaskVM>>(SessionConst.COMBO_TASK_LIST_KEY);
                //foreach(var materialVM in comboMaterialListSession)
                //{

                //    viewModel.Combo.Materials.Add(materialVM.Material);
                //}
                _unitOfWork.Combo.Add(viewModel.Combo); //Add Combo to Combo table
                                                        //Save changes to persist the combo
                _unitOfWork.Save(); //keep track on change
                // Add combo materials to the database
                //step 1: retrieve the combo recently added.
                var addedCombo = _unitOfWork.Combo.GetAllWithFilter(c =>
                c.Name == viewModel.Combo.Name && c.Description == viewModel.Combo.Description).
                FirstOrDefault();
                if (addedCombo != null)
                {
                    //step2: add combo material to the db
                    if (comboMaterialListSession != null)
                    {
                        foreach (var item in comboMaterialListSession)
                        {
                            var comboMaterial = new ComboMaterial() { CombosId = addedCombo.Id, MaterialsId = item.Material.Id };

                            // Add the combo material to the database
                            _unitOfWork.ComboMaterial.Add(comboMaterial);
                            _unitOfWork.Save();
                        }
                    }

                    // step3: Add combo tasks to the database
                    if (comboTaskListSession != null)
                    {
                        foreach (var item in comboTaskListSession)
                        {
                            // set the combosid for the combo task
                            var comboTask = new ComboTask() { CombosId = addedCombo.Id, TasksId = item.Task.Id };

                            // add the combo task to the database
                            _unitOfWork.ComboTask.Add(comboTask);
                            _unitOfWork.Save();
                        }
                    }
                }

            //step4: Remove section after add

                HttpContext.Session.Remove(SessionConst.COMBO_TASK_LIST_KEY);
                HttpContext.Session.CommitAsync();
                HttpContext.Session.Remove(SessionConst.COMBO_MATERIAL_LIST_KEY);
                HttpContext.Session.CommitAsync();
                //toaster notificate
                TempData["success"] = "Combo đã thêm thành công";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                // Handle exception if any
                TempData["error"] = "Đã xảy ra lỗi trong quá trình thêm combo: " + ex.Message;
                return RedirectToAction("Index");
            }

        }
        //Edit Action
        public IActionResult Edit(string? id)
        {
            if (id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1: retrieve Combo from DB
            Combo? comboFromDb = _unitOfWork.Combo.Get(u => u.Id == id);

            //catch not found exception
            if (comboFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            //retrieve material from db based on comboid
            var comboMaterialsFromDb = _unitOfWork.ComboMaterial.GetAll(cm => cm.CombosId == id).ToList();
            var comboTasksFromDb = _unitOfWork.ComboTask.GetAll(ct => ct.CombosId == id).ToList();
            //step 2: pass the comboFromDb obj Properties to comboQuotationVM instance
            //step 2.1: create comboVM
            ComboDetailViewModel comboDetailViewModel = new()
            {
                ConstructionTypeList = _unitOfWork.ConstructionType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Combo = new Combo(),
                ComboMaterials = comboMaterialsFromDb,
                ComboTasks = comboTasksFromDb
            };
            //step 2.1: pass Combo to ComboVM
            comboDetailViewModel.Combo = comboFromDb;
            //load the Combo Material List and ComboTaskList

            //retrieve Material from comboMaterialsFromDb

            var materialViewModelList = new List<MaterialViewModel>();
            if (comboMaterialsFromDb != null)
            {
                foreach (var comboMaterial in comboMaterialsFromDb)
                {

                    var materialFromDb = _unitOfWork.Material.Get(u => u.Id == comboMaterial.MaterialsId && u.Status == true);
                    if (materialFromDb != null)
                    {
                        var materialVM = new MaterialViewModel()
                        {
                            Material = materialFromDb,
                            CategoryName = _unitOfWork.MaterialCategory.GetName(materialFromDb.CategoryId)
                        };
                        // Add the MaterialViewModel to the list

                        materialViewModelList.Add(materialVM);
                    }

                }
            }


            HttpContext.Session.Set(SessionConst.COMBO_MATERIAL_LIST_KEY, materialViewModelList);
            HttpContext.Session.CommitAsync();

            var taskViewModelList = new List<TaskVM>();
            if (comboTasksFromDb != null)
            {
                foreach (var comboTask in comboTasksFromDb)
                {
                    var taskFromDb = _unitOfWork.Task.Get(u => u.Id == comboTask.TasksId && u.Status == true);
                    // Assuming comboMaterial.Material is the Material entity associated with ComboMaterial
                    if (taskFromDb != null)
                    {
                        var taskViewModel = new TaskVM()
                        {
                            Task = taskFromDb,
                            CategoryName = _unitOfWork.TaskCategory.GetName(taskFromDb.CategoryId)
                        };
                        // Add the MaterialViewModel to the list
                        taskViewModelList.Add(taskViewModel);
                    }
                }
            }


            HttpContext.Session.Set(SessionConst.COMBO_TASK_LIST_KEY, taskViewModelList);
            HttpContext.Session.CommitAsync();

            return View(comboDetailViewModel); //return View + retrieved Combo

        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(ComboDetailViewModel viewModel, IFormFile? file)
        {
            try
            {
                //retrieve rootpath
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                //File handle on Update 
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                                                                                                   //product image location
                    string materialPath = Path.Combine(wwwRootPath, @"images\combo");

                    if (!string.IsNullOrEmpty(viewModel.Combo.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, viewModel.Combo.ImageUrl.TrimStart('\\'));
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
                    viewModel.Combo.ImageUrl = @"\images\combo\" + fileName;
                }


                _unitOfWork.Combo.Update(viewModel.Combo); //Update Combo to Combo table
                _unitOfWork.Save(); //keep track on change

                //try to get COMBO_MATERIAL_LIST session 
                var comboMaterialListSession = HttpContext.Session.Get<List<MaterialViewModel>>(SessionConst.COMBO_MATERIAL_LIST_KEY);

                //try to get Combo_Task_List session

                var comboTaskListSession = HttpContext.Session.Get<List<TaskVM>>(SessionConst.COMBO_TASK_LIST_KEY);

                //update newComboMaterialList to dtb
                if(comboMaterialListSession != null) 
                {
                    // Retrieve ComboMaterial entities with the specific ComboId from the database
                    var oldComboMaterial = _unitOfWork.ComboMaterial.GetAll(cm => cm.CombosId == viewModel.Combo.Id);
                    // Remove the retrieved ComboMaterial entities
                    _unitOfWork.ComboMaterial.RemoveRange(oldComboMaterial);
                    foreach (var item in comboMaterialListSession)
                    {
                        var comboMaterial = new ComboMaterial() { CombosId = viewModel.Combo.Id, MaterialsId = item.Material.Id };

                        // Add the combo material from comboMaterialListSession to the database
                        _unitOfWork.ComboMaterial.Add(comboMaterial);
                        _unitOfWork.Save();
                    }

                }

                if (comboTaskListSession != null)
                {
                    // Retrieve ComboMaterial entities with the specific ComboId from the database
                    var oldComboTask = _unitOfWork.ComboTask.GetAll(cm => cm.CombosId == viewModel.Combo.Id);
                    // Remove the retrieved ComboMaterial entities
                    _unitOfWork.ComboTask.RemoveRange(oldComboTask);
                    foreach (var item in comboTaskListSession)
                    {
                        var comboTask = new ComboTask() { CombosId = viewModel.Combo.Id, TasksId = item.Task.Id };

                        // Add the combo task from comboTask to the database
                        _unitOfWork.ComboTask.Add(comboTask);
                        _unitOfWork.Save();
                    }

                }

                HttpContext.Session.Remove(SessionConst.COMBO_TASK_LIST_KEY);
                HttpContext.Session.CommitAsync();
                HttpContext.Session.Remove(SessionConst.COMBO_MATERIAL_LIST_KEY);
                HttpContext.Session.CommitAsync();
                //toaster notificate
                TempData["success"] = "Gói dịch vụ cập nhật thành công";



            }
            catch (Exception ex)
            {
                TempData["error"] = "Đã xảy ra lỗi trong quá trình chỉnh sửa gói dịch vụ: " + ex.Message;
            }

            return RedirectToAction("Index");
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
            TempData["success"] = "Gói dịch vụ đã xóa thành công";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

        #region API CALL
        public IActionResult GetAll()
        {
            List<Combo> objComboList = _unitOfWork.Combo.GetAllWithFilter(filter: cb => cb.Status == true, includeProperties: "Construction").ToList();

            return Json(new { data = objComboList });
        }
        #endregion
    }
}
