using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility
{
    public class SD //static detail
    {
        //từ khóa mở đầu cho request
        public const string requestIdKey = "RF";
        //từ khóa mở đầu cho mỗi id staff tùy theo role
        public const string SellerIdKey = "SL";
        public const string EngineertIdKey = "EN";
        public const string ManagerIdKey = "MG";

        //tempId for table <-> trigger will handle Id generating
        public const string TempId = "id";
        //CustomQuotation Status 
        public const int Preparing = 1;//Seller add construct detail
        public const int Processing = 2;//Engineer add task
        public const int Pending_Approval = 3;//Manager approve task
        public const int Completed = 4;//Completed and be sent to customer
        public const int Cancelled = -1; //customer not listen the call or not interested, this status is only available before Processing (2)
        public const int Rejected = 0; //manager reject task

        //request status
        public const string RequestStatusPending = "Pending";
        public const string RequestStatusApproved = "Approved";
        public const string RequestStatusRejected = "Rejected";

        //roles for application
        public const string Role_Admin = "Admin";
        public const string Role_Manager = "Manager";
        public const string Role_Engineer = "Engineer";
        public const string Role_Seller = "Seller";
        public const string Role_Customer = "Customer";

        //get our own id
        //var claimsIdentity = (ClaimsIdentity)User.Identity;
        //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        /// <summary>
        /// This method is a helper to get Description of Status.
        /// Input is an int of Status.
        /// Return String Description of that Status.
        /// Author: Nguyen Thanh Dat.
        /// </summary>
        /// <param name="status"></param>s
        /// <returns></returns>
        public static string GetQuotationStatusDescription(int status)
        {
            switch (status)
            {
                case -1:
                    return "Cancelled";
                case 0:
                    return "Rejected";
                case 1:
                    return "Preparing";
                case 2:
                    return "Processing";
                case 3:
                    return "Pending Approval";
                case 4:
                    return "Completed";
                default:
                    return "";
            }
        }
    }
}
