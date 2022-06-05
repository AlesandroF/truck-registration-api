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

        public virtual async Task<TEntity> Add(TEntity entity)
            => (await _context.GetDbSet<TEntity>().AddAsync(entity)).Entity;

        public virtual async Task DeleteAsync(int id)
        {
            var element = await GetByIdAsync(id);
            _context.Set<TEntity>().Remove(element);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
            => await _context.GetDbSet<TEntity>().FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetWithFilter(Expression<Func<TEntity, bool>> search)
            => await _context.Set<TEntity>().AsNoTracking().Where(search).ToListAsync();

        public virtual void UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
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