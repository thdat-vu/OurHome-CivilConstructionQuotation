﻿using Microsoft.EntityFrameworkCore;
using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Xml.Linq;

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
        /// <summary>
        ///  The provided code defines a function named Get that retrieves a single entity from a database table, applying filtering and optional property inclusion logic.
        /// </summary>
        /// <param name="filter">body expression ví dụ (x) => x.Id = id </param>
        /// <param name="includeProperties">the name of foreign key you want to load data, which is a comma-separated string</param>
        /// <returns></returns>
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
		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
				query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var incluProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluProp);
                }
            }
            return query.ToList();
        }

        /// <summary>
        /// The function retrieves data from a database table and applies filtering and optional property inclusion logic. 
        /// This function first constructs a query based on the provided DbSet dbSet,apply query and if includeProperties is not null or empty, it iterates through the properties specified in the parameter and includes them in the query using the Include method. 
        ///  Finally, it executes the query and returns the results as a list of objects of type T.
        /// </summary>
        /// <param name="filter">body expression ví dụ (x) => x.Id = id </param>
        /// <param name="includeProperties">the name of foreign key you want to load data, which is a comma-separated string</param>
        /// <returns></returns>
        public IEnumerable<T> GetAllWithFilter(Expression<Func<T, bool>> filter,string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
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
