using System;
using System.Collections.Generic;

namespace SWP391.CHCQS.Model
{
    public partial class TaskCategory
    {
        public TaskCategory()
        {
            //Tasks = new HashSet<Task>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;

        //public virtual ICollection<Task> Tasks { get; set; }
    }
}
