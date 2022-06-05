using AutoMapper;
using MediatR;
using Truck.Registration.Domain.Ports;

namespace Truck.Registration.Application.UseCases.GetById.Handler
{
    public class GetByIdHandler : IRequestHandler<GetByIdCommand, GetByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetByIdResponse> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            var truck = await _unitOfWork.TruckRepository.GetByIdAsync(request.Id);
            return _mapper.Map<GetByIdResponse>(truck);
        }
    }
}