using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class Material_StandardQuotation
    {
        [ForeignKey("Material")]
        public string MaterialId { get; set; }
        public Material Material { get; set; }
        [ForeignKey("StandardQuotation")]
        public string StandardQuotationId { get; set; }
        public StandardQuotation StandardQuotation { get; set; }
    }
}
