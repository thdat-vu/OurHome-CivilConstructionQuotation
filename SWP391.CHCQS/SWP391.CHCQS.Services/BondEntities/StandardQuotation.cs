using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class StandardQuotation
    {
        public string ComboId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        //Construction Type
        [ForeignKey("ConstructionType")]
        public string? ConstructTypeId { get; set; }
        public ConstructionType? ConstrucType { get; set; }
    }
}
