using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Customer
    {
        public Customer()
        {

        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNum { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Username { get; set; } = null!;
    }
}
