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
    public class ProjectImageRepository : Repository<ProjectImage>, IProjectImageRepository
    {
        private readonly SWP391DBContext _db;
        public ProjectImageRepository(SWP391DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProjectImage obj)
        {
            _db.ProjectImages.Update(obj);
        }
    }
}
