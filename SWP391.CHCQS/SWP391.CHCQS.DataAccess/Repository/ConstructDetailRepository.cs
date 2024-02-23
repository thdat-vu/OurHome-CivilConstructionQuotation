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
    public class ConstructDetailRepository : Repository<ConstructDetail>, IConstructDetailRepository
    {
        private readonly SWP391DBContext _db;
        public ConstructDetailRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ConstructDetail obj)
        {
            _db.ConstructDetails.Update(obj);
        }
        public string GetConstructTypeName(string id)
        {
            return _db.ConstructionTypes.Find(id).Name;
        }

		
	}
}
