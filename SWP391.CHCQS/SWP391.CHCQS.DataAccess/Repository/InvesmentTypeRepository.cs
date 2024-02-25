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
	public class InvesmentTypeRepository : Repository<InvestmentType>, IInvestmentTypeRepository
	{
		private readonly SWP391DBContext _db;

		public InvesmentTypeRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(InvestmentType obj)
		{
			_db.InvestmentTypes.Update(obj);
		}

		public void UpdateRange(List<InvestmentType> obj)
		{
			_db.InvestmentTypes.UpdateRange(obj);
		}
		public string GetName(string id) => _db.InvestmentTypes.Find(id).Name;
	}
}
