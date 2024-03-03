using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            Staff = new HashSet<Staff>();
        }
        [Key]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [MaxLength(100)]
        public string Password { get; set; } = null!;
        [MaxLength(30)]
        public string Role { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
