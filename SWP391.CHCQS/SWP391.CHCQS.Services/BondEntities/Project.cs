using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class Project
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Scale { get; set; }
        public double Size { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        //Customer
        [ForeignKey("Customer")]
        public string? CustomerId { get; set; }
        public Account? User { get; set; }
    }
}
