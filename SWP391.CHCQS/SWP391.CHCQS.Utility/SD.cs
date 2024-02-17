using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility
{
    public class SD //static detail
    {
        //CustomQuotation Status -> Default: 0
        public const int Preparing = 1;//Seller add construct detail
        public const int Processing = 2;//Engineer add task
        public const int Pending_Approval = 3;//Manager approve task
        public const int Completed = 4;//Completed and be sent to customer
    }
}
