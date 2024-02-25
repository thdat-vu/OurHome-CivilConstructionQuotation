using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using SWP391.CHCQS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class RequestRepository : Repository<RequestForm>, IRequestRepository
    {
        private readonly SWP391DBContext _db;
        public RequestRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(RequestForm obj)
        {
            _db.RequestForms.Update(obj);
        }
        //function lấy ra tổng số request được gửi có trong database
        public int CountRequestSum()
        {
            return _db.RequestForms.Count();
        }
        //function lấy ra tổng số cancled request có trong database
        public int CountCancleRequestSum()
        {
            //false: request status là đã hủy
            Expression<Func<RequestForm, bool>> filter = (x) => x.Status == false;
            return GetAllWithFilter(filter).Count();
        }
    }
}
