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
    public class RejectedCustomQuotationRepository : Repository<RejectedCustomQuotation>, IRejectedCustomQuotationRepository
    {
        private readonly SWP391DBContext _db;
        public RejectedCustomQuotationRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(RejectedCustomQuotation model)
        {
            _db.RejectedCustomQuotations.Update(model);
        }
    }
}
