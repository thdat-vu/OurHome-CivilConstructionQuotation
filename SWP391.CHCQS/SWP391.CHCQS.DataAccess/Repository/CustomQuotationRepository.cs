using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        //function lấy ra tổng số Custom Quotation dc tạo bởi seller ~ số khách hàng đã sử dụng báo giá
        public int Count()
        {
            return _db.CustomQuotations.Count();
        }

		//function lấy custom quotation bằng id
		public CustomQuotation GetById(string id, string? includeProperties = null)
		{
			Expression<Func<CustomQuotation, bool>> filter = x => x.Id == id;
			return Get(filter, includeProperties);
		}


	}
}
