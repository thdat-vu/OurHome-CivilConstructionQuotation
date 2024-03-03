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
	internal class CustomerRepository : Repository<Customer>, ICustomerRepository
	{
		private readonly SWP391DBContext _db;
		public CustomerRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public string GetName(string id)
		{
			throw new NotImplementedException();
		}

		public void Update(ConstructionType obj)
		{
			throw new NotImplementedException();
		}

		public void UpdateRange(List<ConstructionType> obj)
		{
			throw new NotImplementedException();
		}
	}
}
