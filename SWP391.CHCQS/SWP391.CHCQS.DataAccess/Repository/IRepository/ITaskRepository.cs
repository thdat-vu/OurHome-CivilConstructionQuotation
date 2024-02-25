using SWP391.CHCQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = SWP391.CHCQS.Model.Task;

namespace SWP391.CHCQS.DataAccess.Repository.IRepository
{
    public interface ITaskRepository : IRepository<Task>
    {
        void Update(Task obj);
        string GetName(string id);
    }
}
