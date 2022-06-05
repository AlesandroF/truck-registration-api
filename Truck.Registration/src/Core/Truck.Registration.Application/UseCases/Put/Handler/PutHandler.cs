using AutoMapper;
using MediatR;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Domain.Ports;

namespace Truck.Registration.Application.UseCases.Put.Handler
{
    public class PutHandler : IRequestHandler<PutCommand, PutResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PutHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PutResponse> Handle(PutCommand request, CancellationToken cancellationToken)
        {
            var truck = await _unitOfWork.TruckRepository.GetByIdAsync(request.Id);

            if(truck is null)
                throw new CustomValidationException(new List<string> { "Truck not exist!" });

            _mapper.Map(request, truck);

            _unitOfWork.TruckRepository.UpdateAsync(truck);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<PutResponse>(truck);
        }
    }
}