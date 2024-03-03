using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class TaskDetailRepository : Repository<TaskDetail>, ITaskDetailRepository
    {
        private readonly SWP391DBContext _db;
        public TaskDetailRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(TaskDetail obj)
        {
            _db.TaskDetails.Update(obj);
        }

        public void UpdateRange(List<TaskDetail> obj)
        {
            _db.TaskDetails.UpdateRange(obj);
        }
        public IEnumerable<TaskDetail> GetTaskDetail(string quoteId, string? includeProp = null!)
        {
            return GetAllWithFilter((x) => x.QuotationId == quoteId, includeProp);
        }

    }
}
