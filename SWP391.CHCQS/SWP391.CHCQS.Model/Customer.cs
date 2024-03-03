using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
            RequestForms = new HashSet<RequestForm>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(12), MinLength(9)]
        public string? PhoneNum { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(10)]
        public string? Gender { get; set; }
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        public virtual Account UsernameNavigation { get; set; } = null!;
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<RequestForm> RequestForms { get; set; }
    }
}
