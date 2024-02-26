using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class CustomQuotationRepository : Repository<CustomQuotation>, ICustomQuotationRepository
    {
        private readonly SWP391DBContext _db;
        public CustomQuotationRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CustomQuotation obj)
        {
            _db.CustomQuotations.Update(obj);
        }
        
        //function trả về số quotation dc tạo ra trong tháng
        public int CountCustomQuotationInMonthAndYear(int month, int year)
        {
            //DateTime? thì có thể null nên cần làm khác 1 chút
            Expression<Func<CustomQuotation, bool>> filter = (x) =>x.SubmissionDateSeller.Value.Month == month && x.SubmissionDateSeller.Value.Year == year;
            return GetAllWithFilter(filter).Count() ;
        }
        //public void UpdateStatus(string id, int status)
        //{
        //    var target = Get((x) => x.Id == id);
        //    target.Status = status;
        //    //thực hiện update đối tượng
        //    Update(target);
        //}
    }
}
