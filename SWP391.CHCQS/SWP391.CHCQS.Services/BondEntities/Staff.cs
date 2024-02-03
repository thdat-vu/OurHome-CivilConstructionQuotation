using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    /// <summary>
    /// This class represents employees within an organization, specifically Salers, Engineers, Managers and Admin
    /// Every staff (except Managers) is managed by one manager
    /// </summary>
    public class Staff
    {
        //This is an unique code for each role, each role will have a different opening symbol
        //For example:
        //Saler: SA8888
        //Engineer: EN8888
        //Manager: MN8888
        //Admin: AD8888
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get => FirstName + LastName; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public bool ActivationStatus { get; set; }

        //Cách đọc của khóa ngoại, lấy ví dụ ManagerId
        //-> ta có <ManagerId> là khóa ngoại tham chiếu đến khóa chính <StaffId> ở bảng <Staff>
        //                         FOREIGN KEY REFERENCES  
        //-> <Manager> sẽ là đối tượng thay thế, giữ thông tin cho record của <StaffId> tương ứng ở bảng <Staff>
        //Manager of Staff
        [ForeignKey("Staff")]
        public string? ManagerId { get; set; }
        public Staff? Manager { get; set; }
        //Staff Account
        [ForeignKey("Account")]
        public string? AccountId { get; set; }
        public Account? Account { get; set; }

    }
}
