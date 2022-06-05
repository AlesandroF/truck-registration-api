using MediatR;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Application.UseCases.Add
{
    public class AddCommand : IRequest<AddResponse>
    {
        public int? YearManufacture { get; set; }
        
        public int ModelYear { get; set; }
        
        public TruckModelEnum Model { get; set; }
    }
}