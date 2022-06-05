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
        
        Task<T> Add(T entity);
        
        void UpdateAsync(T entity);
        
        Task DeleteAsync(int id);
        
        Task<IEnumerable<T>> GetWithFilter(Expression<Func<T, bool>> search);

        IQueryable<T> Query(Expression<Func<T, bool>> expression = null, string[] expands = null);
    }
}