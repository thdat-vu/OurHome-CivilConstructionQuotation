using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Account
    {
        public Account()
        {
            Customers = new HashSet<Customer>();
            Staff = new HashSet<Staff>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
