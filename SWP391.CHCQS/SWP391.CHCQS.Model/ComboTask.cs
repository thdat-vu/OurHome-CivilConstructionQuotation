using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public partial class ComboTask
    {
        
        public string CombosId { get; set; }
        
        public string TasksId  { get; set; }
        [ForeignKey("CombosId")]
        public virtual Combo Combo { get; set; } = null!;
        [ForeignKey("TasksId")]
        public virtual Task Task { get; set; } = null!;



    }
}
