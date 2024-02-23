using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface ICustomQuotaionTaskRepository : IRepository<CustomQuotationTask> 
    {
        void Update(CustomQuotationTask obj);
        void UpdateRange(List<CustomQuotationTask> obj);
        IEnumerable<CustomQuotationTask> GetTaskDetail(string quoteId, string? includeProp = null!);

    }
}
