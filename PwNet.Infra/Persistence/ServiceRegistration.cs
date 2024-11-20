using Microsoft.Extensions.DependencyInjection;
using PwNet.Application.Interfaces.Infra.Persistence;
using PwNet.Common.Configurations;
using PwNet.Infra.Persistence.Context;
using PwNet.Infra.Persistence.Repositories;

namespace PwNet.Infra.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSqlPersistence(this IServiceCollection services)
        {
            services
                .AddOptions<DatabaseConfig>()
                .BindConfiguration(nameof(DatabaseConfig));

            return services
                .AddDbContext<SqlContext>(ServiceLifetime.Singleton)
                .AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
