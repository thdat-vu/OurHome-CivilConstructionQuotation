using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class TaskDetail
    {
        public string TaskDetailId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Status  { get; set; }

        //Task Category
        public int? TaskCategoryId { get; set; }
        public TaskCategory? TaskCategory { get; set; }
    }
}
