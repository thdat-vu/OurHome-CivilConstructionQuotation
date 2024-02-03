using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Services.BondEntities
{
    /// <summary>
    /// This class represents user accounts within a system, likely for authentication and authorization purposes.
    /// </summary>
    public class Account
    {
        [Key]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        //Xác thực cơ bản .net cung cấp
        //- Ít nhất một chữ cái viết thường
        //- Ít nhất một chữ cái viết hoa
        //- Ít nhất một chữ số
        //- Ít nhất một ký tự đặc biệt(@, $, !, %, *, ?, &)
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

        public string Role { get; set; }
    }
}