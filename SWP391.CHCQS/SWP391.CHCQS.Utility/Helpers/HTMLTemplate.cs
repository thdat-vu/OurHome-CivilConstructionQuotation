using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Utility.Helpers
{
	public class HTMLTemplate
	{
		public static string Get()
		{
			return @"
    <p>Hello valued customer, we are sending you a response to your request on 24/02/2024. Your total amount due is $XXX, inclusive of the following items:</p>
    
		";
		}
	}
}
