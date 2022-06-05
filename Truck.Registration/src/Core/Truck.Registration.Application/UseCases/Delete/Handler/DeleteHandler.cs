using MediatR;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Domain.Ports;

namespace Truck.Registration.Application.UseCases.Delete.Handler
{
    public class DeleteHandler : IRequestHandler<DeleteCommand, DeleteResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteResponse> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            if(!_unitOfWork.TruckRepository.Query(e => e.Id == request.Id).Any())
                throw new CustomValidationException(new List<string> { "Truck not exist!" });

            await _unitOfWork.TruckRepository.DeleteAsync(request.Id);
            await _unitOfWork.CommitAsync();
            
            return new DeleteResponse
            {
                Id = request.Id,
            };
        }
    }
}