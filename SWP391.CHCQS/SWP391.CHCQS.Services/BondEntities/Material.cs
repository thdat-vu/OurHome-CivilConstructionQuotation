using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class Material
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string Unit { get; set; }
        public int Status { get; set; }
        //Material Category
        [ForeignKey("MaterialCategory")]
        public string MaterialCateId { get; set; }
        public MaterialCategory MaterialCate { get; set; }
    }
}
