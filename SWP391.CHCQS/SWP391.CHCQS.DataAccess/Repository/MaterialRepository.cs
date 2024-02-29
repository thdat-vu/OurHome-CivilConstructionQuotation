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
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private readonly SWP391DBContext _db;

        public MaterialRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Material obj)
        {
            //_db.Materials.Update(obj);
            //step1: retrieve obj from db
            var objFromDb = _db.Materials.SingleOrDefault(u => u.Id == obj.Id);
			//do not use Find<> bcause Find<> for tracking PrimaryKey annotation in Model
			//step2: pass Properties from obj to objFromDb
			if (objFromDb != null) 
            {
				objFromDb.Name = obj.Name;
                objFromDb.InventoryQuantity = obj.InventoryQuantity;
                objFromDb.UnitPrice = obj.UnitPrice;
                objFromDb.Unit = obj.Unit;
                objFromDb.CategoryId = obj.CategoryId;
			}
        }
        public string GetName(string id) => _db.Materials.Find(id).Name;
  
    }
}
