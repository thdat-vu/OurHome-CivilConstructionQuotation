using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
	public interface IFoundationTypeRepository : IRepository<FoundationType>
	{
		void Update(FoundationType obj);
		void UpdateRange(List<FoundationType> obj);
	}
}
