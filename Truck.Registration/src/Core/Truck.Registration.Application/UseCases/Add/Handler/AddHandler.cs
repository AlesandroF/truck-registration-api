using MediatR;
using Truck.Registration.Application.Exceptions;
using Truck.Registration.Application.Helper;
using Truck.Registration.Domain.Ports;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.Application.UseCases.Add.Handler
{
    public class AddHandler : IRequestHandler<AddCommand, AddResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddResponse> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            await AddValidate(request);

            var truckSaved = await _unitOfWork.TruckRepository.Add(
                new Entity.Truck
                {
                    Model = request.Model,
                    ModelYear = request.ModelYear,
                    YearManufacture = request.YearManufacture.HasValue ? request.YearManufacture.Value : DateTime.UtcNow.Year,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Active = true
                });

            await _unitOfWork.CommitAsync();

            return new AddResponse
            {
                Id = truckSaved.Id,
                Model = truckSaved.Model.GetDescription(),
                ModelYear = truckSaved.ModelYear,
                YearManufacture = truckSaved.YearManufacture
            };
        }

        public async Task AddValidate(AddCommand request)
        {
            if ((await _unitOfWork.TruckRepository.GetWithFilter(x => x.ModelYear == request.ModelYear && x.YearManufacture == request.YearManufacture && x.Model == request.Model && x.Active)).Any())
                throw new CustomValidationException(new List<string> { "Truck already exists!" });
        }
    }
}