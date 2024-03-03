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
    public class ComboRepository : Repository<Combo>, IComboRepository
    {
        private readonly SWP391DBContext _db;
        public ComboRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Combo obj)
        {
			//_db.Combos.Update(obj);
			//step 1: retrieve object from database 
			var objFromDb = _db.Combos.SingleOrDefault(u => u.Id == obj.Id);
			
			//step 2: pass Properties from obj to objFromDb
			if (objFromDb != null)
			{
				objFromDb.Name = obj.Name;
				objFromDb.Description = obj.Description;
				objFromDb.Price = obj.Price;
				objFromDb.ConstructionId = obj.ConstructionId;
			}
		}
    }
}
