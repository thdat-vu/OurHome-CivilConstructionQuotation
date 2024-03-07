using Microsoft.AspNetCore.Mvc.Rendering;
using SWP391.CHCQS.Model;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Admin.ViewModels
{
	public class RoleManagementVM
	{
		public ApplicationUser ApplicationUser { get; set; }
		public IEnumerable<SelectListItem> RoleList { get; set; }
		public IEnumerable<SelectListItem> ManagerList { get; set; }
	}
}
