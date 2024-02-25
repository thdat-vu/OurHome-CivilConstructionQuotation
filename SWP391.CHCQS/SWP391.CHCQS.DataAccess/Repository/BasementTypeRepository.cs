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
	public class BasementTypeRepository : Repository<BasementType>, IBasementTypeRepository
	{
		private readonly SWP391DBContext _db;
		public BasementTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(BasementType obj)
		{
			_db.BasementTypes.Update(obj);
		}

		public void UpdateRange(List<BasementType> obj)
		{
			_db.BasementTypes.UpdateRange(obj);
		}
		public string GetName(string id) => _db.BasementTypes.Find(id).Name;
	}
}
