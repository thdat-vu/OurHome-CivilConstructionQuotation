using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.DBInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly SWP391DBContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(SWP391DBContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public void Initialize()
		{
			//migrations if they are not applied
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}

			//create roles if they are not created
			if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Engineer)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Seller)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();

				//if roles are not created, then will create admin user as well
				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "admin@gmail.com",
					Email = "admin@gmail.com",
					Name = "Admin",
					PhoneNumber = "1234567890"
				}, "123456aA!").GetAwaiter().GetResult();

				ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}
			
			return;
		}
	}
}
