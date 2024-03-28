using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System.Collections.Generic;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ComboController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComboController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            //var comboList = _unitOfWork.Combo.GetAll(includeProperties: "Materials,Tasks").ToList();
            //TODO: lấy đc combo + MaterialList TaskList mỗi combo.

            // Retrieve all combos with their associated materials and tasks
           var comboList =  _unitOfWork.Combo.GetAll(x => x.Status == true).ToList();
            #region Fix bug
            // //DatVT, bruce-force way.
            // //step1: retrieve the ComboMaterials<ComboIds, MaterialsId> and ComboTask<CombosId, TasksId> according to ComboId
            // //Step2: mapping corresponding MaterialsId and TasksId to retrieve MaterialList and TaskList based on Id
            // //step3: AddRange to Materials Property and Tasks Property of each Combo Model.
            // foreach (var combo in comboList)
            //{
            //     //retrieve a comboMaterials list on ComboMaterial Entity
            //     var comboMaterials = _unitOfWork.ComboMaterial.GetAll(cm => cm.CombosId == combo.Id).ToList();
            //     //create a MaterialList with data type is List of Material.
            //     var materialList = new List<Material>();
            //     //manually add every material to Materials Property in Combo Model
            //     foreach (var material in comboMaterials)
            //     {
            //         materialList.Add(_unitOfWork.Material.Get(m => m.Id == material.MaterialsId));
            //     }
            //     if(materialList.Count > 0)
            //     {
            //         combo.Materials.AddRange(materialList);
            //     }
            //     //retrieve a comboTasks list on ComboTasks Entity
            //     var comboTasks = _unitOfWork.ComboTask.GetAll(ct => ct.CombosId == combo.Id).ToList();
            //     // create a MaterialList with data type is List of Material.
            //     var taskList = new List<Task>();
            //     //manually Add every task to Tasks Property in Combo Model
            //     foreach (var task in comboTasks)
            //     {
            //         taskList.Add(_unitOfWork.Task.Get(t => t.Id == task.TasksId));
            //     }
            //     if (taskList.Count > 0)
            //     {
            //         combo.Tasks.AddRange(taskList);
            //     }
            // }
            #endregion
            return View(comboList);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var combo = _unitOfWork.Combo.Get(x => x.Id == id);

            //retrieve a comboMaterials list on ComboMaterial Entity
            var comboMaterials = _unitOfWork.ComboMaterial.GetAll(cm => cm.CombosId == combo.Id).ToList();
            //create a MaterialList with data type is List of Material.
            var materialList = new List<Material>();
            //manually add every material to Materials Property in Combo Model
            foreach (var material in comboMaterials)
            {
                materialList.Add(_unitOfWork.Material.Get(m => m.Id == material.MaterialsId));
            }
            if (materialList.Count > 0)
            {
                combo.Materials.AddRange(materialList);
            }

            //retrieve a comboTasks list on ComboTasks Entity
            var comboTasks = _unitOfWork.ComboTask.GetAll(ct => ct.CombosId == combo.Id).ToList();
            // create a MaterialList with data type is List of Material.
            var taskList = new List<Task>();
            //manually Add every task to Tasks Property in Combo Model
            foreach (var task in comboTasks)
            {
                taskList.Add(_unitOfWork.Task.Get(t => t.Id == task.TasksId));
            }
            if (taskList.Count > 0)
            {
                combo.Tasks.AddRange(taskList);
            }
            return View(combo);
        }
    }
}
