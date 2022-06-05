using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Truck.Registration.Domain.Ports
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);
        
        T Add(T entity);
        
        Task UpdateAsync(T entity, int id);
        
        Task DeleteAsync(int id);
        
        Task<IEnumerable<T>> GetWithFilter(Expression<Func<T, bool>> search);

        IQueryable<T> Query(Expression<Func<T, bool>> expression = null, string[] expands = null);
    }
}