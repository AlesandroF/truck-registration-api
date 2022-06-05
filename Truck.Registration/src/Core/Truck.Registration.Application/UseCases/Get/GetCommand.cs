using MediatR;

namespace Truck.Registration.Application.UseCases.Get
{
    public class GetCommand : IRequest<List<GetResponse>>
    {
    }
}