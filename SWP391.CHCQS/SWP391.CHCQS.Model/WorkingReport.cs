﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model
{
    public class WorkingReport
    {
        [Key]
        public int Id { get; set; }

        public string StaffId { get; set; } = null!;
        [ForeignKey("StaffId")]
        public virtual ApplicationUser Staff { get; set; } = null!;

        [MaxLength(10)]
        public string RequestId { get; set; } = null!;
        [ForeignKey("RequestId")]
        public virtual RequestForm RequestForm { get; set; } = null!;

        public DateTime? ReceiveDate { get; set; }
        public DateTime? SubmitDate { get; set; }

    }
}
