using System.Diagnostics.CodeAnalysis;

namespace Truck.Registration.Application.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class CustomValidationException : Exception
    {
        public IEnumerable<string> ErrosMessage { get; set; }

        public CustomValidationException(string message) : base(message) { }

        public CustomValidationException(IEnumerable<string> errosMessage)
        {
            ErrosMessage = errosMessage;
        }
    }
}