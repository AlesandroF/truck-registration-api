using Microsoft.EntityFrameworkCore;
using Truck.Registration.Domain.Ports;
using Truck.Registration.MySql.Context;

namespace Truck.Registration.MySql.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MySqlContext _context;

        private ITruckRepository _truckRepository;

        public UnitOfWork(MySqlContext context)
        {
            _context = context;
        }

        public ITruckRepository TruckRepository => _truckRepository ??= new TruckRepository(_context);

        public async Task<bool> CommitAsync()
        {
            var commited = true;
            if (!_context.HasChanges())
                return false;

            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var dbContextTransaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch
                {
                    await dbContextTransaction.RollbackAsync();
                    commited = false;
                }
            });

            return commited;
        }
    }
}