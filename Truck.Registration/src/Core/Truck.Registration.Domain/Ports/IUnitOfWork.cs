using System.Threading.Tasks;

namespace Truck.Registration.Domain.Ports
{
    public interface IUnitOfWork
    {
        ITruckRepository TruckRepository { get; }

        Task<bool> CommitAsync();
    }
}