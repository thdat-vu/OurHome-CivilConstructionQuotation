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
		public const int GetAllFilterStatus = 5; //sử dụng cho việc query ra tất cả các custom quotation 
		#endregion

		
		#region Request Status
		public const string RequestStatusPending = "Đang xử lí";
        public const string RequestStatusSaved = "Đã lưu";
        public const string RequestStatusSent = "Đã gửi cho kỹ sư";
        public const string RequestStatusRejected = "Đã từ chối";

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
		public static List<string> Alleys = new () { "Lớn hơn 5m", "Từ 3m - 5m", "Nhỏ hơn 3m", "Không có hẻm" };
        //options for facade in QuickQuote
        public static List<int> Facades = new () { 0, 1, 2, 3, 4 };
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

        //Construction type id
        public const string ConstructionType_TownHouse = "CT1";
        public const string ConstructionType_Mansion = "CT2";
        public const string ConstructionType_Grade4House = "CT3";
        
		#endregion

		#region Material Constants
		public static List<string> MaterialUnits = new() { "mét" , "mét vuông", "mét khối", "tấn", "thùng", "bao", "viên", "cái" };
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
                    return "Đã hủy";
                case 0:
                    return "Đã từ chối";
                case 1:
                    return "Chuẩn bị";
                case 2:
                    return "Đang xử lí";
                case 3:
                    return "Đợi xác nhận";
                case 4:
                    return "Hoàn thành";
                default:
                    return "";
            }
        }

        public static string GetRoleInVietNamese(string role)
        {
            switch (role)
            {
                case Role_Admin:
                    return "Quản trị viên";
                case Role_Manager:
                    return "Quản lý";
                case Role_Engineer:
                    return "Kỹ sư";
                case Role_Seller:
                    return "Tư vấn viên";
                case Role_Customer:
                    return "Khách hàng";
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

        #region Enum
        //gender list
        public enum GenderList
        {
            Nam,
            Nữ,
            Khác
        }


        #endregion
    }
}
