using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class InvestmentType
    {
        [Key]
        public string InvestTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}