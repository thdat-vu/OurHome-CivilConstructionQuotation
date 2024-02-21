using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class RequestForm
    {
        [Key]
        public string Id { get; set; }
        public DateTime GenerateDate { get; set; }
        public string? Description { get; set; }

        public string TypeOfConstruct { get; set; }
        public  double  Acreage { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        //Customer
        public string CustomerId { get; set; }
        public Customer User { get; set; }
    }
}