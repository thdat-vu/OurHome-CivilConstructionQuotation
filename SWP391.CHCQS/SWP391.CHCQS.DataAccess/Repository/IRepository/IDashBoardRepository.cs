using SWP391.CHCQS.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    //class tổng hợp dữ liệu để trả về Chart class
    public class IDashBoardRepository
    {
        private readonly SWP391DBContext _db;

        public IDashBoardRepository(SWP391DBContext db)
        {
            _db = db;
        }
        //
    }
}
