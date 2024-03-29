using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public class ApplicationUser :IdentityUser
    {        
        [MaxLength(100)]
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Tên")]
        public string Name { get; set; } = null!;

        [MaxLength(10)]
        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }

        public string? ConnectionId { get; set; }
        [Display(Name = "Quản lý")]
		public string? ManagerId { get; set; } = null!;
		[ForeignKey("ManagerId")]

		public virtual ApplicationUser? Manager { get; set; } = null!;

		[NotMapped]
        [Display(Name = "Vai trò")]
		public string Role { get; set; } = null!;
	}
}
