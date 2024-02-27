using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;


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