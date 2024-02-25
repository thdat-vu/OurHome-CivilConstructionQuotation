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
            _db.Materials.Update(obj);
        }
        public string GetName(string id) => _db.Materials.Find(id).Name;
  
    }
}
