using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstractions
{
    public interface IRepository<T> where T:class
    {
        Task<T> get(int id);
        void delete(T entity);
        Task<IEnumerable<T>> getAll(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null, bool descOrder = true);
        T create(T entity);
        T update(T entity);
        
    }
}
