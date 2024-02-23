using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;


namespace SWP391.CHCQS.DataAccess.Repository
{
	public class ConstructionTypeRepository : Repository<ConstructionType>, IConstructionTypeRepository
	{
		private readonly SWP391DBContext _db;

		public ConstructionTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public string GetName(string id)
		{
			return _db.ConstructionTypes.Find(id).Name;
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
