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

        //function lấy ra tổng số request được gửi có trong database theo tháng
        public int CountRequestInMonthAndYear(int month, int year)
        {
            Expression<Func<RequestForm, bool>> filter = (x) => x.GenerateDate.Month == month && x.GenerateDate.Year == year;
            return GetAllWithFilter(filter).Count();
        }
        //function lấy ra tổng số cancled request có trong database theo tháng
        public int CountCancledRequestInMonthAndYear(int month, int year)
        {   //false: request status là đã hủy
            Expression<Func<RequestForm, bool>> filter = (x) => x.GenerateDate.Month == month && x.GenerateDate.Year == year && x.Status == SD.RequestStatusRejected;
            return GetAllWithFilter(filter).Count();
        }
        //function trả về danh sách các năm có có tồn tại request
        public IEnumerable<int> GetYearList()
        {
            //lấy ra danh sách request dc sắp xếp theo GenerateDate tăng dần
            var orderedList = _db.RequestForms.OrderBy((x) => x.GenerateDate).ToList();
            //biến giữ năm to nhất
            int currentMaxYear = 0;
            //duyệt vòng lặp qua list để trả về 1 danh sách gồm các năm
            var yearList = new List<int>();

            foreach (var x in orderedList)
            {
                //nếu năm trong từng request lớn hơn currentYearMax thì sẽ dc add vào yearList và sau đó gán lại giá trị cho currentYearMAx
                if (x.GenerateDate.Year > currentMaxYear)
                {
                    yearList.Add(x.GenerateDate.Year);
                    currentMaxYear = x.GenerateDate.Year;
                }
            }
            return yearList;

        }
    }
}
