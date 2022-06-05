using MediatR;

namespace Truck.Registration.Application.UseCases.Delete
{
    public class DeleteCommand : IRequest<DeleteResponse>
    {
        public int Id { get; set; } 
    }
}