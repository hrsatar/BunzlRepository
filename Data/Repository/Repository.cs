using Data.Abstractions;
using Data.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _context;
        DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }

        public T create(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void delete(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async virtual Task<T> get(int id)
        {
            var obj=await _dbSet.FindAsync(id);
            return obj;
        }

        public async Task<IEnumerable<T>> getAll(Expression<Func<T,bool>> filter=null,Expression<Func<T,object>> orderBy=null,bool descOrder=true)
        {
            IQueryable<T> query = _dbSet;

            if(filter!=null)
            {
                query = query.Where(filter);
            }

            if(orderBy!=null)
            {
                if (descOrder)
                {
                    query=query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            return await query.ToListAsync();
        }

        public T update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
