using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface IComboTaskRepository : IRepository<ComboTask>
    {
        void Update(ComboTask obj);
        void UpdateRange(List<ComboTask> obj);
        IEnumerable<ComboTask> GetComboTask(string comboId, string? includeProp = null!);
    }
}
