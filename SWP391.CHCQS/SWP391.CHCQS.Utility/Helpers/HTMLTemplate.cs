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
    <h2>Task Detail</h2>
    <table style='border: 1px solid #000'>
        <thead>
            <tr>
                <th>Column 1</th>
                <th>Column 2</th>
                <th>Column 3</th>
                <th>Column 4</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Data 1</td>
                <td>Data 2</td>
                <td>Data 3</td>
                <td>Data 4</td>
            </tr>
            <!-- Add more rows as needed -->
        </tbody>
    </table>
    <h2>Material Detail</h2>
    <table style='border: 1px solid #000'>
        <thead>
            <tr>
                <th>Column 1</th>
                <th>Column 2</th>
                <th>Column 3</th>
                <th>Column 4</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Data 1</td>
                <td>Data 2</td>
                <td>Data 3</td>
                <td>Data 4</td>
            </tr>
            <!-- Add more rows as needed -->
        </tbody>
    </table>
";
		}
	}
}
