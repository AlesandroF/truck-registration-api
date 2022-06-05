using FluentValidation;
using Truck.Registration.Domain.Enums;

namespace Truck.Registration.Application.UseCases.Put
{
    public class PutCommandValidator : AbstractValidator<PutCommand>
    {
        public PutCommandValidator()
        {
            RuleFor(c => c.ModelYear)
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow.Year)
                .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
                .WithMessage("Required to enter a model year valid.");

            RuleFor(c => c.Model).NotNull().NotEqual(x => TruckModelEnum.FMX).WithMessage("Model not permitted.");
            RuleFor(c => c.Id).NotNull().GreaterThan(0).WithMessage("Identifier unique not informed.");
        }
    }
}