using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface IComboMaterialRepository : IRepository<ComboMaterial>
    {
        void Update(ComboMaterial obj);
        void UpdateRange(List<ComboMaterial> obj);
        IEnumerable<ComboMaterial> GetComboMaterial(string comboId, string? includeProp = null!);
    }
}
