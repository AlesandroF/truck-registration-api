using System.Diagnostics.CodeAnalysis;

namespace Truck.Registration.Application.ValueObject
{
    [ExcludeFromCodeCoverage]
    public class AppSettings
    {
        public string ConnectionString { get; set; }
    }
}