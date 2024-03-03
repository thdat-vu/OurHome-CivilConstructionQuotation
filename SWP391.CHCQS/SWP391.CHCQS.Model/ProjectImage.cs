using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public class ProjectImage
    {
        [Key]
        [MaxLength(10)]
        public int Id { get; set; }
        [MaxLength(200)]
        public string ImageUrl { get; set; }
        [MaxLength(10)]
        public string ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
