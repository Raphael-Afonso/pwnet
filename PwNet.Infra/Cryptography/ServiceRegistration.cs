using Microsoft.Extensions.DependencyInjection;
using PwNet.Application.Interfaces.Infra;

namespace PwNet.Infra.Cryptography
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCryptographyServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<IPasswordCryptography, BCryptoCryptography>();
        }
    }
}
