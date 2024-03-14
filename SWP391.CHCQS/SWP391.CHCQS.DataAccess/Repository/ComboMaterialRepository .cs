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
    public class ComboMaterialRepository : Repository<ComboMaterial>, IComboMaterialRepository
    {
        private readonly SWP391DBContext _db;
        public ComboMaterialRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        

		public void Update(ComboMaterial obj)
		{
			_db.ComboMaterials.Update(obj);
		}

		public void UpdateRange(List<ComboMaterial> obj)
		{
			_db.ComboMaterials.UpdateRange(obj);
		}

		public IEnumerable<ComboMaterial> GetComboMaterial(string comboId, string? includeProp = null)
		{
			return GetAllWithFilter((x) => x.CombosId == comboId, includeProp);
		}
	}
}
