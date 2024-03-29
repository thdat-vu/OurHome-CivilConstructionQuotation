using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
		public async Task<IActionResult> Index(int page = 1)
		{
			int pageSize = 10; // Số lượng combo trên mỗi trang
							   // Tính toán offset để lấy dữ liệu từ database cho trang hiện tại
			int offset = (page - 1) * pageSize;

			// Lấy danh sách dự án từ database
			List<Combo> comboList = _unitOfWork.Combo.GetAll()
				.Where(x => x.Status == true)
				.Skip(offset)
				.Take(pageSize)
				.ToList();

			// Tính toán tổng số trang dựa trên tổng số dự án
			int totalProjects = _unitOfWork.Project.GetAll().Count();
			int totalPages = (int)Math.Ceiling((double)totalProjects / pageSize);

			ViewBag.CurrentPage = page;
			ViewBag.TotalPages = totalPages;

			return View(comboList);
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
		}

		public async Task<IActionResult> Detail(string? id)
		{
			if (id != null)
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
			else
			{
				TempData["Error"] = "Không thể xem gói dịch vụ lúc này";
				return RedirectToAction(nameof(Index));
			}
		}
        public IActionResult Search(string? keyword, int page = 1)
        {
            if (keyword == null)
            {
				TempData["Error"] = "Bạn chưa nhập từ khóa tìm kiếm gói dịch vụ";
                return RedirectToAction(nameof(Index));
            }
            var keyTrim = keyword.Trim().ToLower();
            int pageSize = 10; // Số lượng dự án trên mỗi trang
                               // Tính toán offset để lấy dữ liệu từ database cho trang hiện tại
            int offset = (page - 1) * pageSize;

            var comboList = _unitOfWork.Combo.GetAll(x => x.Status == true)
                .Where(x =>
                    (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(keyTrim)) ||
                    (!string.IsNullOrEmpty(x.Description) && x.Description.ToLower().Contains(keyTrim))
                    ).ToList();

            var displayList = comboList.Skip(offset)
                                        .Take(pageSize)
                                        .ToList();

            // Tính toán tổng số trang dựa trên tổng số dự án
            int totalCombos = comboList.Count();
            int totalPages = (int)Math.Ceiling((double)totalCombos / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.LastSearch = keyword;
            return View("Index", displayList);
        }
    }
        
}
