using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391.CHCQS.Model
{
    public partial class RequestForm
    {
        public RequestForm()
        {
        }
        [Key]
        [MaxLength(10)]
        public string Id { get; set; } = null!;
        public DateTime GenerateDate { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(30)]
        public string? ConstructType { get; set; }
        [MaxLength(30)]
        public string? Acreage { get; set; }
        [MaxLength(200)]
        public string Location { get; set; } = null!;
        //true: vẫn dag xử lý, còn hiệu lực
        //false: ko còn hiệu lực
        public bool Status { get; set; }
        [MaxLength(10)]
        public string CustomerId { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public CustomQuotation? CustomQuotation { get; set; }
    }
}
