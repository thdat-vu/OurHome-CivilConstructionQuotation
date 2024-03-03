﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(12), MinLength(9)]
        public string? PhoneNum { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
    }
}
