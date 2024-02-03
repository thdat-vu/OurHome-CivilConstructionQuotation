using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class MaterialCategory
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}