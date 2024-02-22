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
	public class RoofTypeRepository : Repository<RooftopType>, IRoofTypeRepository
	{
		private readonly SWP391DBContext _db;
		public RoofTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(RooftopType obj)
		{
			_db.RooftopTypes.Update(obj);
		}

		public void UpdateRange(List<RooftopType> obj)
		{
			_db.RooftopTypes.UpdateRange(obj);
		}
		public string GetName(string id) => _db.RooftopTypes.Find(id).Name;
	}
}
