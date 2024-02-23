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
    public class MaterialDetailRepository : Repository<MaterialDetail>, IMaterialDetailRepository
    {
        private readonly SWP391DBContext _db;
        public MaterialDetailRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MaterialDetail obj)
        {
            _db.MaterialDetails.Update(obj);
        }
        public void UpdateRange(List<MaterialDetail> obj)
        {
            _db.MaterialDetails.UpdateRange(obj);
        }
        public IEnumerable<MaterialDetail> GetMaterialDetail(string quoteId, string? includeProp = null!)
        {
            return GetAllWithFilter((x) => x.QuotationId == quoteId, includeProp);
        }
        
    }
}
