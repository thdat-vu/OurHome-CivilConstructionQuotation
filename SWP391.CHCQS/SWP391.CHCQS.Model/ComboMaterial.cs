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

		
		public string CombosId { get; set; }
		
		public string MaterialsId { get; set; }

        [ForeignKey("CombosId")]
        public virtual Combo Combo { get; set; } = null!;
        [ForeignKey("MaterialsId")]
        public virtual Material Material { get; set; } = null!;
    }
}
