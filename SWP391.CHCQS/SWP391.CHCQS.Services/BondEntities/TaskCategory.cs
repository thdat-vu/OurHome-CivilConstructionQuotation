using System.ComponentModel.DataAnnotations;

namespace SWP391.CHCQS.Services.BondEntities
{
    public class TaskCategory
    {
        //identity
        [Key]
        public int TaskCategoryId { get; set; }
        [Key] 
        
        public int TaskId { get; set; }

        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}