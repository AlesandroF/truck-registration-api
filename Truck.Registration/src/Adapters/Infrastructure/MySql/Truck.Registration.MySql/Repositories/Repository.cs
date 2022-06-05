using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Truck.Registration.Domain.Ports;
using Truck.Registration.MySql.Context;

namespace Truck.Registration.MySql.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly MySqlContext _context;

        protected Repository(MySqlContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();

        public virtual TEntity Add(TEntity entity)
            => (_context.GetDbSet<TEntity>().Add(entity)).Entity;

        public virtual async Task DeleteAsync(int id)
        {
            var element = await GetByIdAsync(id);
            _context.Set<TEntity>().Remove(element);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
            => await _context.GetDbSet<TEntity>().FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetWithFilter(Expression<Func<TEntity, bool>> search)
            => await _context.Set<TEntity>().AsNoTracking().Where(search).ToListAsync();

        public virtual async Task UpdateAsync(TEntity entity, int id)
        {
            if (entity == null) return;
            var existing = await GetByIdAsync(id);

            if (existing != null)
                _context.Entry(existing).CurrentValues.SetValues(entity);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null, string[] expands = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

            if (expands != default)
            {
                foreach (var exp in expands)
                    query = query.Include(exp);
            }

            if (expression == null)
                return query;

            return query.Where(expression);
        }
    }
}