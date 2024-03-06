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
        [Required]
        public string Name { get; set; } = null!;

        [MaxLength(10)]
        public string? Gender { get; set; }

        public string? ConnectionId { get; set; }

		public string? ManagerId { get; set; } = null!;
		[ForeignKey("ManagerId")]

		public virtual ApplicationUser? Manager { get; set; } = null!;
	}
}
