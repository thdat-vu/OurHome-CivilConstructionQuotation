using Org.BouncyCastle.Asn1.Ocsp;
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
    public class WorkingReportRepository : Repository<WorkingReport>, IWorkingReportRepository
    {
        private readonly SWP391DBContext _db;
        public WorkingReportRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(WorkingReport obj)
        => _db.WorkingReports.Update(obj);

      public List<WorkingReport> GetAll()
        => _db.WorkingReports.ToList();
		
	}
}
