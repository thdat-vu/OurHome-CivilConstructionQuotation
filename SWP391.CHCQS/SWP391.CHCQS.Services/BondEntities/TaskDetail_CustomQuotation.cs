using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class TaskDetail_CustomQuotation
    {
        [ForeignKey("CustomQuotation")]
        public string CustomQuotationId { get; set; }
        public CustomQuotation CustomQuotation { get; set; }
        [ForeignKey("TaskDetail")]
        public int TaskDetailId { get; set; }
        public TaskDetail TaskDetail { get; set; }

    }
}
