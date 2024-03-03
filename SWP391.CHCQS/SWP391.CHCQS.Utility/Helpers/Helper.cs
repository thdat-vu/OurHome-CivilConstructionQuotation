using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility.Helpers
{
    public class Helper
    {
        public static string TransferId(string Id, string replaceStr) => $"{replaceStr}{Id.Substring(2)}";
    }
}
