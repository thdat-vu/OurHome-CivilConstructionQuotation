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

        public IActionResult GetCustomerDetails(string id)
        {
            //retrieve Detail of customer
            var customerDetail = _unitOfWork.ApplicationUser.Get((x) => x.Id == id);


            if (customerDetail == null)
            {
                return NotFound(); // Or handle the case where customerDetail is null
            }
            //create CustomerVM based on customerDetail
            var customerDetailVM = new CustomerVM()
            {
                Id = id,
                Name = customerDetail.Name

            };
            //TODO: Test result
            return Json(new { data = customerDetailVM });
        }

        #region APICALLGETCUSTOMER

        /// <summary>
        /// This method return a Json of List of customer so that Customer Data table can read.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAll()
        {
            //lấy list user thuộc role customer : data type là identity
            var identityUserList = await _userManager.GetUsersInRoleAsync(SD.Role_Customer);
            //tạo danh sách id
            var idList = new List<string>();
            foreach (var user in identityUserList)
            {
                idList.Add(user.Id);
            }
            //đưa về kiểu application user vì thuộc tính name chỉ có ở application user
            //kiểm tra xem id có nằm trong idList hay ko, nếu có thì lấy.
            var applicationUserList = _unitOfWork.ApplicationUser.GetAll(x => idList.Contains(x.Id) ).ToList();
            

            //dùng phép chiếu đưa application user về VM bằng cách chỉ lấy thuộc tính cần sử dụng
            List<CustomerVM> list = new List<CustomerVM>();
            applicationUserList.ForEach(x => list.Add(new CustomerVM { Id = x.Id, Name = x.Name }));

            return Json(new { data = list });
        }

        

        
        #endregion
    }
}
