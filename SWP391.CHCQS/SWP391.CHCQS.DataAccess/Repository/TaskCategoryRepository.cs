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
	public class TaskCategoryRepository : Repository<TaskCategory>, ITaskCategoryRepository
	{
		private readonly SWP391DBContext _db;
		public TaskCategoryRepository(SWP391DBContext db) : base(db)
		{
			_db = db;
		}

		public void Update(TaskCategory obj)
		{
			_db.TaskCategories.Update(obj);
		}
	}
}
