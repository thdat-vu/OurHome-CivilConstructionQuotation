using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
