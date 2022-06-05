using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Truck.Registration.Domain.Ports;
using Truck.Registration.MySql.Context;
using Truck.Registration.MySql.Repositories;

namespace Truck.Registration.MySql.Module
{
    public static class MySqlModuleDependency
    {
        public static void AddMySqlModule(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddMySqlContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MySqlContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), x => x.MigrationsAssembly("Truck.Registration.MySql")));
        }
    }
}