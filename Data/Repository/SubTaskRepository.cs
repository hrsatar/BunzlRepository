using Data.Abstractions;
using Data.AppContext;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SubTaskRepository:Repository<SubTask>,ISubTaskRepository
    {
        ApplicationDbContext _context;

        public SubTaskRepository(ApplicationDbContext context):base(context) 
        {
            _context=context;
        }
    }
}
