using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
            //RequestForms = new HashSet<RequestForm>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PhoneNum { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string Username { get; set; } = null!;

        public virtual Account UsernameNavigation { get; set; } = null!;
        public virtual ICollection<Project> Projects { get; set; }
        //public virtual ICollection<RequestForm> RequestForms { get; set; }
    }
}
