using Data.Abstractions;
using Data.AppContext;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {

        ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Taskk = new TaskRepository(_context);
            SubTask = new SubTaskRepository(_context);
        }

        public ITaskRepository Taskk { get; set; }
        public ISubTaskRepository SubTask { get; set; }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
