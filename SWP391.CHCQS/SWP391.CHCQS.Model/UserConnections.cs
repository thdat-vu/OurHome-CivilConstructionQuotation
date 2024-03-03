using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public class UserConnections
    {
        [Key]
        [MaxLength(10)]
        public string UserId { get; set; }
        [MaxLength(200)]
        public string ConnectionId { get; set; }
    }
}
