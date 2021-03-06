using FluentValidation;
using MediatR;
using System.Diagnostics.CodeAnalysis;
using Truck.Registration.Application.Exceptions;

namespace Truck.Registration.Application.Pipeline
{
    [ExcludeFromCodeCoverage]
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Select(x => x.ErrorMessage);

                if (failures.Any())
                    throw new CustomValidationException(failures);
            }

            return await next();
        }
    }
}