using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface IComboMaterialRepository : IRepository<ComboMaterials>
    {
        void Update(ComboMaterials obj);
        void UpdateRange(List<ComboMaterials> obj);
        IEnumerable<ComboMaterials> GetComboMaterial(string comboId, string? includeProp = null!);
    }
}
