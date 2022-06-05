using Microsoft.EntityFrameworkCore;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.MySql.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
        }

        public virtual DbSet<Entity.Truck> Truck { get; set; }

        public DbSet<T> GetDbSet<T>() where T : class => Set<T>();
        public bool HasChanges() => ChangeTracker.HasChanges();
    }
}