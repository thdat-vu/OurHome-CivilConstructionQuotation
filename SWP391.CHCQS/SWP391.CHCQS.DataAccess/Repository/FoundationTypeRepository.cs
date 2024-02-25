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
	public class FoundationTypeRepository : Repository<FoundationType>, IFoundationTypeRepository
	{
		private readonly SWP391DBContext _db;

		public FoundationTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(FoundationType obj)
		{
			_db.FoundationTypes.Update(obj);
		}

		public void UpdateRange(List<FoundationType> obj)
		{
			_db.FoundationTypes.UpdateRange(obj);
		}
		public string GetName(string id)
		{
			return _db.FoundationTypes.Find(id).Name;
		}
	}
}
