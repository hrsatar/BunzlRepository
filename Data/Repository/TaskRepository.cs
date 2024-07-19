using Data.Abstractions;
using Data.AppContext;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class TaskRepository: Repository<Data.Models.Taskk>,ITaskRepository
    {
        ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

       public async Task<Data.Models.Taskk> getTaskWithSubTasks(int taskId)
       {
            var obj= await _context.Tasks.Include(t=>t.subTasks).FirstOrDefaultAsync(t=>t.taskId == taskId);
            return obj;
       }

        public async override Task<Data.Models.Taskk> get(int taskId)
        {
            var obj = await _context.Tasks.Select(t=>new Taskk { taskId = t.taskId,name=t.name,completed=t.completed,subTaskCount=t.subTasks.Count() }).FirstOrDefaultAsync(t=>t.taskId==taskId);
            return obj;
        }


        public async Task<IEnumerable<Data.Models.Taskk>> getAllWithSubTaskCount(Expression<Func<Data.Models.Taskk,bool>> filter=null,Expression<Func<Data.Models.Taskk,object>> orderBy=null,bool descOrder=true)
        {
            IQueryable<Data.Models.Taskk> query = _context.Tasks;
            query = query.Select(t=>new Models.Taskk { completed=t.completed,taskId=t.taskId,name=t.name,subTaskCount=t.subTasks.Count()});
            if(filter!=null)
            {
                query=query.Where(filter);
            }
            if(orderBy!=null)
            {
                if(descOrder)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query=query.OrderBy(orderBy);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Data.Models.Taskk>> getAllWithSubTasks(Expression<Func<Data.Models.Taskk, bool>> filter = null, Expression<Func<Data.Models.Taskk, object>> orderBy = null, bool descOrder = true)
        {
            IQueryable<Data.Models.Taskk> query = _context.Tasks;
            query = query.Include(t=>t.subTasks);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                if (descOrder)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            Console.WriteLine(query.ToQueryString());
            return await query.ToListAsync();
        }

    }
}
