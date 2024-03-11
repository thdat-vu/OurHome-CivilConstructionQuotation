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
		#region Prefix
		//từ khóa mở đầu cho request
		public const string requestIdKey = "RF";
        //từ khóa mở đầu cho quotation
        public const string quotationIdKey = "CQ";
		#endregion


		//tempId for table <-> trigger will handle Id generating
		public const string TempId = "id";

		#region CustomQuotation Status

		public const int Preparing = 1;//Seller add construct detail
		public const int Processing = 2;//Engineer add task
		public const int Pending_Approval = 3;//Manager approve task
		public const int Completed = 4;//Completed and be sent to customer
		public const int Cancelled = -1; //customer not listen the call or not interested, this status is only available before Processing (2)
		public const int Rejected = 0; //manager reject task

		#endregion


		#region Request Status

		public const string RequestStatusPending = "Pending";
        public const string RequestStatusApproved = "Approved";
        public const string RequestStatusRejected = "Rejected";

		#endregion

		#region Roles For Application

		public const string Role_Admin = "Admin";
        public const string Role_Manager = "Manager";
        public const string Role_Engineer = "Engineer";
        public const string Role_Seller = "Seller";
        public const string Role_Customer = "Customer";

		#endregion

		#region QuickQuote Constants
		//options for alley in QuickQuote
		public static List<string> Alleys = new () { "Wider than 5m", "Width from 3m - 5m", "Less than 3m" };
		//surcharge for alley less than 5m
		public const int AlleySurcharge = 100_000;

        //default area for balcony in QuickQuote
        public const int DefaultBalconyArea = 9;
		//Area coefficient of mezzanine pine
        public const double MezzanineCoefficient = 0.5;
		//Area coefficient of the terrace
        public const double TerraceCoefficient = 0.3;
		//Area coefficient of the garden
        public const double GardenCoefficient = 0.7;

        //Basement id of No Basement option
        public const string NoBasementId = "BT1";

		#endregion

		#region Functions

		/// <summary>
		/// This medthod is a helper to get current user id.
		/// </summary>
		/// <param name="user"></param>
		/// <returns>user id</returns>
		public static string GetCurrentUserId(ClaimsPrincipal user)
        {
			var claimsIdentity = (ClaimsIdentity)user.Identity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
		

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

        #endregion

        #region Material Category Id

        public const string Category_Raw = "VT1";
        public const string Category_Paint = "VT2";
        public const string Category_Electric = "VT3";
        public const string Category_Sanitary = "VT4";
        public const string Category_Kitchen = "VT5";
        public const string Category_Stair = "VT6";
        public const string Category_Door = "VT7";
        public const string Category_CeramicTiles = "VT8";
        public const string Category_Ceiling = "VT9";

        #endregion

        //gender list
        public enum GenderList
        {
            Male,
            Female,
            Other
        }

       
        
    }
}
