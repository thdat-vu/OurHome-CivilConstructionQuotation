using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Admin.ViewModels;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Admin.Controllers
{
    [Area(SD.Role_Admin)]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly SWP391DBContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		public UserController(SWP391DBContext db, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public IActionResult Index()
        {
            return View();
        }
		public IActionResult RoleManagement(string userId)
		{
			var roleId = _db.UserRoles.FirstOrDefault(x => x.UserId == userId).RoleId;
			var ManagerIdList = _userManager.GetUsersInRoleAsync(SD.Role_Manager)
				.GetAwaiter().GetResult().Select(x => x.Id).ToList();

			RoleManagementVM roleVM = new RoleManagementVM()
			{
				ApplicationUser = _db.ApplicationUsers.Include(x => x.Manager).FirstOrDefault(x => x.Id == userId),
				RoleList = _db.Roles.Select(i => new SelectListItem { Text = i.Name, Value = i.Name }),
				ManagerList = _db.ApplicationUsers.Where(x => ManagerIdList.Contains(x.Id))
				.Select(i => new SelectListItem { Text = i.Name, Value = i.Id })
			};
			roleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(x => x.Id == roleId).Name;

			return View(roleVM);
		}
		[HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleVM)
        {
            string roleId = _db.UserRoles.FirstOrDefault(x => x.UserId == roleVM.ApplicationUser.Id).RoleId;
			string oldrole = _db.Roles.FirstOrDefault(x => x.Id == roleId).Name;
			if (roleVM.ApplicationUser.Role != oldrole)
			{
				//a role was updated
				var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == roleVM.ApplicationUser.Id);
				if (roleVM.ApplicationUser.Role == SD.Role_Seller || roleVM.ApplicationUser.Role == SD.Role_Engineer)
				{
                    user.ManagerId = roleVM.ApplicationUser.ManagerId;
                }
                else
				{
                    user.ManagerId = null;
                }
				_db.SaveChanges();

				_userManager.RemoveFromRoleAsync(user, oldrole).GetAwaiter().GetResult();
				_userManager.AddToRoleAsync(user, roleVM.ApplicationUser.Role).GetAwaiter().GetResult();
			}

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            List<ApplicationUser> userList = _db.ApplicationUsers.Include(x => x.Manager).ToList();
			var userRole = _db.UserRoles.ToList();
			var roles = _db.Roles.ToList();

			userList.ForEach(u =>
			{
				var roleId = userRole.FirstOrDefault(x => x.UserId == u.Id).RoleId;
				u.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;
				if (u.Manager == null)
				{
					u.Manager = new ApplicationUser() { Name = "" };
				}
			});
			return Json(new { data = userList });
        }

		[HttpPost]
		public IActionResult LockUnlock([FromBody] string id)
		{
			var userFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
			if (userFromDb == null)
			{
				return Json(new { success = false, message = "Đã xảy ra lỗi khi khóa / mở khóa tài khoản" });
			}
			if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
			{
				//user is currently locked, we will unlock them
				userFromDb.LockoutEnd = DateTime.Now;
			}
			else
			{
				userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
			}
			_db.SaveChanges();
			return Json(new { success = true, message = "Cập nhật thành công" });
		}
		#endregion
	}
}
