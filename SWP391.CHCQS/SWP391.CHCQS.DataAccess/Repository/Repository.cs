using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SWP391DBContext _db;
        internal DbSet<T> dbSet;
        public Repository(SWP391DBContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(List<T> entity)
        {
            dbSet.AddRange(entity);
        }

        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                //Ví dụ truyền vào includePreperties: "ConstructionType,BasementType" thì nó sẽ tách chuỗi rồi Inlcude();
                foreach (var incluProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluProp);
                }
            }
            return query.FirstOrDefault();
        }
		/// <summary>
		/// The function retrieves all objects of type T from the database. It optionally includes related data specified by the includeProperties parameter
		/// This function first constructs a query based on the provided DbSet dbSet, and if includeProperties is not null or empty, it iterates through the properties specified in the parameter and includes them in the query using the Include method. 
        /// Finally, it executes the query and returns the results as a list of objects of type T.
		/// </summary>
		/// <param name="includeProperties">the name of foreign key you want to load data, which is a comma-separated string.</param>
		/// <returns>IEnumerable<T></returns>
		public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var incluProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluProp);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
