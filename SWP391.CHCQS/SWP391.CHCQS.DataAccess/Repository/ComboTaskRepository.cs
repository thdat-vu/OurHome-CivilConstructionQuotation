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
    public class ComboTaskRepository : Repository<ComboTask>, IComboTaskRepository
    {
        private readonly SWP391DBContext _db;
        public ComboTaskRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        

		public void Update(ComboTask obj)
		{
			_db.ComboTasks.Update(obj);
		}

		public void UpdateRange(List<ComboTask> obj)
		{
			_db.ComboTasks.UpdateRange(obj);
		}

		public IEnumerable<ComboTask> GetComboTask(string comboId, string? includeProp = null)
		{
			return GetAllWithFilter((x)=> x.CombosId == comboId, includeProp);
		}
	}
}
