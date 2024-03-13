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
        [Key]
        public string CombosId { get; set; }
        
        [Key]
        public string TasksId  { get; set; }
        [ForeignKey("CombosId")]
        public virtual Combo Combo { get; set; }
        [ForeignKey("TasksId")]
        public virtual Task Task { get; set; }


    }
}
