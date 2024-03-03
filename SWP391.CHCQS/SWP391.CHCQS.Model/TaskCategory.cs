using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Model
{
    public partial class TaskCategory
    {
        public TaskCategory()
        {
            //Tasks = new HashSet<Task>();
        }
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        //public virtual ICollection<Task> Tasks { get; set; }
    }
}
