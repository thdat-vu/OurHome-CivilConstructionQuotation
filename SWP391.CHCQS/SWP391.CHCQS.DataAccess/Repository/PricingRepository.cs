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
	public class PricingRepository : Repository<Pricing>, IPricingRepository
	{
		private readonly SWP391DBContext _db;
		public PricingRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Project obj)
		{
			_db.Projects.Update(obj);
		}
	}
}
