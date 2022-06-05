using MediatR;

namespace Truck.Registration.Application.UseCases.GetById
{
    public class GetByIdCommand : IRequest<GetByIdResponse>
    {
        public int Id { get; set; } 
    }
}