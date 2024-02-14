using SWP391.CHCQS.DataAccess.Data;
using SWP391.CHCQS.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SWP391DBContext _db;
        public IStandardQuotationRepository StandardQuotation {  get; private set; }
        public IProjectRepository Project {  get; private set; }
        public UnitOfWork(SWP391DBContext db)
        {
            _db = db;
            StandardQuotation = new StandardQuotationRepository(_db);
            Project = new ProjectRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
