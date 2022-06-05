using Truck.Registration.Domain.Ports;
using Truck.Registration.MySql.Context;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.MySql.Repositories
{
    public class TruckRepository : Repository<Entity.Truck>, ITruckRepository
    {
        public TruckRepository(MySqlContext context) : base(context)
        {
        }
    }
}