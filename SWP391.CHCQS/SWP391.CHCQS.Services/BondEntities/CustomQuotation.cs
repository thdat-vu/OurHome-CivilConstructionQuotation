using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class CustomQuotation
    {
        [Key]
        public string QuotationId { get; set; }
        public DateTime Date { get; set; }
        public double Acreage { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public string? Description { get; set; } 
        public double? Total { get; set; }
        //Saler
        [ForeignKey("Staff")]
        public string? SalerId { get; set; }
        public Staff? Saler { get; set; }
        //Manager
        [ForeignKey("Staff")]
        public string? ManagerId { get; set; }
        public Staff? Manager { get; set; }
        //Engineer
        [ForeignKey("Staff")]
        public string? EngineerId { get; set; }
        public Staff? Engineer { get; set; }
        //RequestForm
        public string? RequestId { get; set; }
        public RequestForm? Request { get; set; }

        //Construct Detail
        public string? ConstructDetailId { get; set; }
        public ConstructDetail? ConstructDetail { get; set; }
    }
}
