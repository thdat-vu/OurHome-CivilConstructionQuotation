using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{    
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IComboRepository Combo { get; }
        IComboMaterialRepository ComboMaterial { get; }
        IComboTaskRepository ComboTask { get; }
        IProjectRepository Project { get; }
        ITaskDetailRepository TaskDetail { get; }
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
        IMaterialCategoryRepository MaterialCategory { get; }
        IRejectedCustomQuotationRepository RejectedCustomQuotation { get; }

        IWorkingReportRepository WorkingReport { get; }
        IPricingRepository Pricing { get; }

        void Save();
    }
}
