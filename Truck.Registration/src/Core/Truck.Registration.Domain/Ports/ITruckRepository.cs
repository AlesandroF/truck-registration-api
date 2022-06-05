using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Domain.Ports
{
    public interface ITruckRepository : IRepository<Entity.Truck>
    {
    }
}