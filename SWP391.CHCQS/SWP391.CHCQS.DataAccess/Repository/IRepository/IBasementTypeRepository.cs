using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
	public interface IBasementTypeRepository : IRepository<BasementType>
	{
		void Update(BasementType obj);
		void UpdateRange(List<BasementType> obj);
		string GetName(string id);
	}
}
