using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace SWP391.CHCQS.Services
{
    public class AppState
    {
        
        public static Tuple<int, int, int> State(int slCount, int enCount, int mgCount)
        {
            
            Random random = new Random();
            int slId = random.Next(1, slCount);
            int enId = random.Next(1, enCount);
            int mgId = random.Next(1, mgCount);



            return Tuple.Create(slId, enId, mgId);
        }
    }
}
