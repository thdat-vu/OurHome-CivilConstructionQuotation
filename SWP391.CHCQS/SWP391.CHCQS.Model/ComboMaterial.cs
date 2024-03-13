using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public partial class ComboMaterial
    {
        [Key]
        public string CombosId { get; set; }

        [Key]
        public string MaterialsId { get; set; }
        [ForeignKey("CombosId")]
        public virtual Combo Combo { get; set; }
        [ForeignKey("MaterialsId")]
        public virtual Material Material { get; set; }
    }
}
