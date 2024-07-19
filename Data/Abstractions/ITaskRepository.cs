using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstractions
{
    public interface ITaskRepository : IRepository<Data.Models.Taskk>
    {
        Task<Data.Models.Taskk> getTaskWithSubTasks(int taskId);
        Task<Data.Models.Taskk> get(int taskId);
        Task<IEnumerable<Data.Models.Taskk>> getAllWithSubTaskCount(Expression<Func<Data.Models.Taskk, bool>> filter = null, Expression<Func<Data.Models.Taskk, object>> orderBy = null, bool descOrder = true);
        Task<IEnumerable<Data.Models.Taskk>> getAllWithSubTasks(Expression<Func<Data.Models.Taskk, bool>> filter = null, Expression<Func<Data.Models.Taskk, object>> orderBy = null, bool descOrder = true);
    }
}
