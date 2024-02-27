using SWP391.CHCQS.Model;


namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface IRejectedCustomQuotationRepository : IRepository<RejectedCustomQuotation>
    {
        void Update(RejectedCustomQuotation model);
    }
}