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
    public class CustomQuotaionTaskRepository : Repository<CustomQuotationTask>, ICustomQuotaionTaskRepository
    {
        private readonly SWP391DBContext _db;
        public CustomQuotaionTaskRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CustomQuotationTask obj)
        {
            _db.CustomQuotaionTasks.Update(obj);
        }

        public void UpdateRange(List<CustomQuotationTask> obj)
        {
            _db.CustomQuotaionTasks.UpdateRange(obj);
        }
		
	}
}
