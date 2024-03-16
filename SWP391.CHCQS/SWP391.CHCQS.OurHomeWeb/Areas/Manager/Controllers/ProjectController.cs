using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;
using System.Drawing;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

//DatVT
namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
    //register Area
    [Area(SD.Role_Manager)]
    [Authorize(Roles = SD.Role_Manager)]
    public class ProjectController : Controller
    {
        //init IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //inject IWebHostEnvironment to access wwwroot folder
        //ctor
        public ProjectController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }




        public IActionResult Index()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAll().ToList();
            //return View();
            return View(objProjectList); //redirect to Index.cshtml + objList
        }


        public IActionResult Create()
        {
            //add ProjectViewModel to pass Properties.
            ProjectVM productVM = new();
            return View(productVM);
        }

        //Create request HttpPOST
        [HttpPost]
        public IActionResult Create(ProjectVM projectVM, List<IFormFile>? files)
        {
            
            projectVM.Status = true; //change status into true;
            projectVM.Id = SD.TempId;
            DateTime parsedDate;
            if (DateTime.TryParseExact(Request.Form["Date"], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                projectVM.Date = parsedDate;
            }
            else
            {
                ModelState.AddModelError("Date", "Sai định dạng ngày tháng");
            }


            Project project = new Project()
            {
                Id = projectVM.Id,
                Name = projectVM.Name,
                Location = projectVM.Location,
                Scale = projectVM.Scale,
                Size = projectVM.Size,
                Status = projectVM.Status,
                Description = projectVM.Description,
                Overview = projectVM.Overview,
                Date = projectVM.Date,
                CustomerId = projectVM.CustomerId

            };
            _unitOfWork.Project.Add(project); //Add ProjectVM to Project table
            _unitOfWork.Save(); //keep track on change
            string wwwRootPath = _webHostEnvironment.WebRootPath; //retrieve rootpath.
            //handle file 
            if (files != null)
            {
                foreach (IFormFile file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                    string projectId = _unitOfWork.Project.Get(p => p.Name == projectVM.Name && p.Status == true && p.CustomerId == projectVM.CustomerId).Id;
                    if (projectId != null)
                    {
                        string projectImagesPath = @"images\project\project-" + projectId;
                        string finalPath = Path.Combine(wwwRootPath, projectImagesPath);
                        //if the projectimagesPath does not exist -> Create the directory
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProjectImage projectImage = new ProjectImage() 
                        {
                            ImageUrl = @"\" + projectImagesPath + @"\" + fileName,
                            ProjectId = projectId,
                        };

                        if(projectVM.Images == null)
                        {
                            projectVM.Images = new List<ProjectImage>();
                        }
                        projectVM.Images.Add(projectImage);
                        _unitOfWork.ProjectImage.Add(projectImage); //add projectimages.
                        _unitOfWork.Save(); //keep track on change
                    }
                }               
            }


            
            TempData["success"] = "Thêm dự án thành công";
            return RedirectToAction("Index"); //after adding, return to previous action and reload the page

            //return View(projectVM); //return previous action + invalid object
        }
        //Edit Action
        public IActionResult Edit(string? id)
        {
            if (id == null) //id is null
            {
                return NotFound();//return status not found aka 404
            }
            //step 1:retrieve Project from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == id, includeProperties: "Customer");

            //catch not found exception
            if (projectFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            //step 2: pass the Material projectVM Properties to MaterialViewModel instance
            //step 2.1: create MaterialViewModel
            ProjectVM projectVM = new()
            {
                Id = projectFromDb.Id,
                Name = projectFromDb.Name,
                Location = projectFromDb.Location,
                Scale = projectFromDb.Scale,
                Size = projectFromDb.Size,
                Status = projectFromDb.Status,
                Description = projectFromDb.Description,
                Overview = projectFromDb.Overview,
                Date = projectFromDb.Date,
                CustomerId = projectFromDb.CustomerId,
                CustomerName = projectFromDb.Customer.Name,
                Images = _unitOfWork.ProjectImage.GetAll(i => i.ProjectId == projectFromDb.Id).ToList()
                //Project = projectFromDb,
                //CustomerId = projectFromDb.CustomerId,
                //CustomerName = projectFromDb.Customer.Name

            };
            //step 2.1: pass ProjectVM to ProjectVM
            //projectVM.Material = materialFromDb;
            return View(projectVM); //return View + retrieved 
        }
        //Edit request with HttpPOST method
        [HttpPost]
        public IActionResult Edit(ProjectVM projectVM, List<IFormFile>? files)
        {



            //step 1:retrieve Project from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == projectVM.Id, includeProperties: "Customer");
            //catch not found exception
            if (projectFromDb == null)
            {
                return NotFound();//return status not found aka 404
            }
            projectFromDb.Name = projectVM.Name;
            projectFromDb.Location = projectVM.Location;
            projectFromDb.Scale = projectVM.Scale;
            projectFromDb.Size = projectVM.Size;
            projectFromDb.Description = projectVM.Description;
            projectFromDb.Overview = projectVM.Overview;
            projectFromDb.Date = projectVM.Date;
            projectFromDb.CustomerId = projectVM.CustomerId;
            _unitOfWork.Save(); //keep track on change

            string wwwRootPath = _webHostEnvironment.WebRootPath; //retrieve rootpath.
            //handle file 
            if (files != null)
            {
                foreach (IFormFile file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//save a new name for filename
                    string projectId = _unitOfWork.Project.Get(p => p.Name == projectVM.Name && p.Status == true && p.CustomerId == projectVM.CustomerId).Id;
                    if (projectId != null)
                    {
                        string projectImagesPath = @"images\project\project-" + projectId;
                        string finalPath = Path.Combine(wwwRootPath, projectImagesPath);
                        //if the projectimagesPath does not exist -> Create the directory
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProjectImage projectImage = new ProjectImage()
                        {
                            ImageUrl = @"\" + projectImagesPath + @"\" + fileName,
                            ProjectId = projectId,
                        };

                        if (projectVM.Images == null)
                        {
                            projectVM.Images = new List<ProjectImage>();
                        }
                        projectVM.Images.Add(projectImage);
                        _unitOfWork.ProjectImage.Add(projectImage); //add projectimages.
                        _unitOfWork.Save(); //keep track on change
                    }
                }
            }


            TempData["success"] = "Chỉnh sửa dự án thành công";
            return RedirectToAction("Index"); //after updating, return to previous action and reload the page

        }
        //Delete Action
        public IActionResult Delete(string? id)
        {
            //catch null id exception
            if (id == null)
            {
                return NotFound(); //return status not found aka 404
            }

            //retrieve projectFromDb from DB
            Project? projectFromDb = _unitOfWork.Project.Get(u => u.Id == id, includeProperties: "Customer");

            if (projectFromDb == null)
            {
                return NotFound(); //return status not found aka 404
            }

            return View(projectFromDb); //return Delete.cshtml + projectFromDb


        }
        //delete request HttpPost, actionname=Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(string? id)
        {
            //retrieve Material from Db
            Project? obj = _unitOfWork.Project.Get(u => u.Id == id);
            //handle id null exception
            if (obj == null)
            {
                return NotFound();//return status not found aka 404
            }

            //change the status in to false
            obj.Status = false;
            _unitOfWork.Save();//keep track on change
            TempData["success"] = "Xóa dự án thành công";
            return RedirectToAction("Index"); //redirect to Index.cshtml
        }

        public IActionResult DeleteImage(int imageId)
        {
            //find the image 
            var imageSelectedToDelete = _unitOfWork.ProjectImage.Get(i => i.Id == imageId);
            var projectId = imageSelectedToDelete.ProjectId;
            if (imageSelectedToDelete != null)
            {
                //delete in wwwrootpath folder
                if (!string.IsNullOrEmpty(imageSelectedToDelete.ImageUrl))
                {
                    var oldImagePath =
                        Path.Combine(_webHostEnvironment.WebRootPath, imageSelectedToDelete.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete( oldImagePath);
                    }
                }
                //remove the record under dtb
                _unitOfWork.ProjectImage.Remove(imageSelectedToDelete);
                _unitOfWork.Save();

                TempData["success"] = "Xóa ảnh thành công";
            }
            return RedirectToAction(nameof (Edit), new {id = projectId });
        }


        #region API CALLS
        /// <summary>
        /// this method return a Json Of Project List so that Project DataTable can read.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Project> objProjectList = _unitOfWork.Project.GetAllWithFilter(filter: p => p.Status == true, includeProperties: "Customer").ToList();
            return Json(new { data = objProjectList }); //json + material list for data table.
        }


        #endregion
    }

}
