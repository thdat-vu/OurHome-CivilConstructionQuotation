using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services
{
    public interface IAppState
    {
        public Tuple<int, int, int> GetDelegationIndex();
    }
}
