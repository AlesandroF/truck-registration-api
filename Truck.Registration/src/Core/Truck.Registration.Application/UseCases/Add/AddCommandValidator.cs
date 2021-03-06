using FluentValidation;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Application.UseCases.Add
{
    public class PutCommandValidator : AbstractValidator<AddCommand>
    {
        public PutCommandValidator()
        {
            RuleFor(c => c.ModelYear)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Year)
                .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
                .WithMessage("Required to enter a model year valid.");

            RuleFor(c => c.Model).NotNull().NotEqual(x => TruckModelEnum.FMX).WithMessage("Model not permitted.");
        }
    }
}