using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.OurHomeWeb.Areas.Manager.ViewModels;
using SWP391.CHCQS.Utility;

namespace SWP391.CHCQS.OurHomeWeb.Areas.Manager.Controllers
{
	[Area("Manager")]
	public class CustomerController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;
        public CustomerController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
			_unitOfWork = unitOfWork;
			_userManager = userManager;

		}
        public IActionResult Index()
		{
			return View();
		}

		#region APICALLGETCUSTOMER
		public async Task<IActionResult> GetAll()
		{
			//lấy list user thuộc role customer : data type là identity
			var identityUserList = await _userManager.GetUsersInRoleAsync(SD.Role_Customer);
			//đưa về kiểu application user vì thuộc tính name chỉ có ở application user
			var applicationUserList = new List<ApplicationUser>();
			foreach (var user in identityUserList)
			{
				applicationUserList.Add(_unitOfWork.ApplicationUser.Get(x => x.Id == user.Id));
			}
			//dùng phép chiếu đưa application user về VM bằng cách chỉ lấy thuộc tính cần sử dụng
			List<CustomerVM> list = new List<CustomerVM>();
			applicationUserList.ForEach(x => list.Add(new CustomerVM { Id = x.Id, Name = x.Name }));

			return Json(new {data = list });
		}

        [HttpGet]
        [ActionName("Detail")]
        public IActionResult GetCustomerDetails([FromQuery] string CustomerId)
        {
			//retrieve Detail of customer
            var customerDetail = _unitOfWork.ApplicationUser.Get((x) => x.Id == CustomerId);
			//create CustomerVM based on customerDetail
			var customerDetailVM = new CustomerVM()
			{
				Id = CustomerId,
				Name = customerDetail.Name

            };
            //TODO: Test result
            return Json(new { data = customerDetailVM });
        }
        #endregion
    }
}
