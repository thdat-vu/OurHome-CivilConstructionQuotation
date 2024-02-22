using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{    
    public interface IUnitOfWork
    {
        IStandardQuotationRepository StandardQuotation { get; }
        IProjectRepository Project { get; }
        ICustomQuotaionTaskRepository CustomQuotaionTask { get; }
        ICustomQuotationRepository CustomQuotation { get; }
        ITaskRepository Task { get; }
        IMaterialRepository Material { get; }
        IMaterialDetailRepository MaterialDetail { get; }
        IConstructDetailRepository ConstructDetail { get; }
        ITaskCategoryRepository TaskCategory { get; }
        IRequestRepository RequestForm { get; }
        IConstructionTypeRepository ConstructionType { get; }
        IInvestmentTypeRepository InvestmentType { get; }
        IFoundationTypeRepository FoundationType { get; }
        IBasementTypeRepository BasementType { get; }
        IRoofTypeRepository RoofType { get; }
        void Save();
    }
}
