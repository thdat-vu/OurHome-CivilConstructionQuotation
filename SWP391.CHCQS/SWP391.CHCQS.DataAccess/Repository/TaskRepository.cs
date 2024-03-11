using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        private readonly SWP391DBContext _db;
        public TaskRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Task obj)
        {
            //_db.Tasks.Update(obj);
            //step1: retrieve Taskobj from DB
            Task? objtaskFromDb = _db.Tasks.SingleOrDefault(u => u.Id == obj.Id);
			if (objtaskFromDb != null)
			{
				objtaskFromDb.Name = obj.Name;
				objtaskFromDb.Description = obj.Description;
				objtaskFromDb.UnitPrice = obj.UnitPrice;
				objtaskFromDb.CategoryId = obj.CategoryId;
			}
		}
        public string GetName(string id) => _db.Tasks.Find(id).Name;
    }
}
