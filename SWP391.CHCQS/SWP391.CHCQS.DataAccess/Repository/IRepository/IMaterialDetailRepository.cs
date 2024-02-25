using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface IMaterialDetailRepository : IRepository<MaterialDetail>
    {
        void Update(MaterialDetail obj);
        void UpdateRange(List<MaterialDetail> obj);
        IEnumerable<MaterialDetail> GetMaterialDetail(string quoteId, string? includeProp = null!);
    }
}
