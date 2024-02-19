using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    //Mục đích class: Tạo ra 1 repository tổng để có thể gọi các repository khác -> dễ mantain code hơn
    public interface IUnitOfWork
    {
        
        public IStandardQuotationRepository StandardQuotation {  get; }
        public IStaffRepository Staff { get; }
        void Save();
    }
}
