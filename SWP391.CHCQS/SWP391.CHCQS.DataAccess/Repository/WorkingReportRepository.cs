using Org.BouncyCastle.Asn1.Ocsp;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public WorkingReport GetBaseOnRequestAndStaffKey(string requestId, string staffKeyId)
        => Get((x) => x.RequestId == requestId && x.StaffId.StartsWith(staffKeyId), "Staff");

        public string GetStaffNameBaseOnRequestAndStaffKey(string requestId, string staffKeyId)
         => GetBaseOnRequestAndStaffKey(requestId, staffKeyId).Staff.Name;

        public void Update(WorkingReport obj)
        => _db.WorkingReports.Update(obj);

    }
}
