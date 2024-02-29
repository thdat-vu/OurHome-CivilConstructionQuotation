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
    public class StandardQuotationRepository : Repository<StandardQuotation>, IStandardQuotationRepository
    {
        private readonly SWP391DBContext _db;
        public StandardQuotationRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(StandardQuotation obj)
        {
            _db.StandardQuotations.Update(obj);
        }
    }
}
