using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
	public interface IInvestmentTypeRepository : IRepository<InvestmentType>
	{
		void Update(InvestmentType obj);
		void UpdateRange(List<InvestmentType> obj);
	}
}
