using AutoMapper;
using MediatR;
using Truck.Registration.Domain.Ports;

namespace Truck.Registration.Application.UseCases.Get.Handler
{
    public class GetHandler : IRequestHandler<GetCommand, List<GetResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GetResponse>> Handle(GetCommand request, CancellationToken cancellationToken)
        {
            var trucks = await _unitOfWork.TruckRepository.GetAllAsync();
            return _mapper.Map<List<GetResponse>>(trucks);
        }
    }
}