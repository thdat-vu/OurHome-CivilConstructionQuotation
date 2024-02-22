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
	public class ConstructionTypeRepository : Repository<ConstructionType>, IConstructionTypeRepository
	{
		private readonly SWP391DBContext _db;

		public ConstructionTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(ConstructionType obj)
		{
			_db.ConstructionTypes.Update(obj);
		}

		public void UpdateRange(List<ConstructionType> obj)
		{
			_db.ConstructionTypes.UpdateRange(obj);
		}
	}
}
