using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Account
    {
        public Account()
        {
        }
        [Key]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [MaxLength(100)]
        public string Password { get; set; } = null!;
        [MaxLength(30)]
        public string Role { get; set; } = null!;

        public virtual Customer? Customers { get; set; }
        public virtual Staff? Staff { get; set; }
    }
}
