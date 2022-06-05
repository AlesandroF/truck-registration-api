using FluentValidation;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Application.UseCases.Add
{
    public class AddCommandValidator : AbstractValidator<AddCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(c => c.ModelYear).NotNull().GreaterThan(0).WithMessage("Required to enter a model year.");
            RuleFor(c => c.Model).NotNull().NotEqual(x => TruckModelEnum.FMX).WithMessage("Model not permitted.");
        }
    }
}