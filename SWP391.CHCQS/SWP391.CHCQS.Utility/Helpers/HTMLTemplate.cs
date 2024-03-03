using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility.Helpers
{
	public class HTMLTemplate
	{
		public static string Get(string name, string generateDate, decimal total)
		{
			return @$"
    <p>Hello {name}, we are sending you a response to your request on {generateDate}. Your total amount due is {total}đ, inclusive of the following items:</p>";
		}
	}
}
