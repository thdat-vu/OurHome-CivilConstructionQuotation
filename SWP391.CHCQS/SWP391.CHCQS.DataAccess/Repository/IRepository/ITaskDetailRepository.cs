using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface ITaskDetailRepository : IRepository<TaskDetail> 
    {
        void Update(TaskDetail obj);
        void UpdateRange(List<TaskDetail> obj);
        IEnumerable<TaskDetail> GetTaskDetail(string quoteId, string? includeProp = null!);

    }
}
