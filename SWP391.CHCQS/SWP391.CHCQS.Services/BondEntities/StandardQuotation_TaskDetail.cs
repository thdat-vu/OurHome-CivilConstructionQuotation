using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class StandardQuotation_TaskDetail
    {
        [ForeignKey("StandardQuotation")]
        public string StandardQuotationId { get; set; }
        public StandardQuotation StandardQuotation { get; set; }
        [ForeignKey("TaskDetail")]
        public int TaskDetailId { get; set; }
        public TaskDetail TaskDetail { get; set; }
    }
}
