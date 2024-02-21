using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
	public class StaffRepository : Repository<Staff>, IStaffRepository
	{
		private readonly SWP391DBContext _db;
		public StaffRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}
		public Staff GetById(string id, string? includeProperties = null)
		{
			Expression<Func<Staff, bool>> filter = x => x.Id == id;
			return Get(filter, includeProperties);
		}
	}
}
