using MediatR;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Application.UseCases.Put
{
    public class PutCommand : IRequest<PutResponse>
    {
        public int Id { get; set; } 

        public int YearManufacture { get; set; }

        public int ModelYear { get; set; }

        public TruckModelEnum Model { get; set; }
    }
}