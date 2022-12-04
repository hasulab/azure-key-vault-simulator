using Azure.KeyVault.Simulator.Models;
using System.Reflection;
using MediatR;

namespace Azure.KeyVault.Simulator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKeyVaultSimulatorServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.Configure<KeyVaultConfig>(settings =>
            {
                configuration.GetSection(nameof(KeyVaultConfig)).Bind(settings);
            });

            var settings = new KeyVaultConfig();
            configuration.GetSection(nameof(KeyVaultConfig)).Bind(settings);
            services.AddSingleton<KeyVaultConfig>(settings);
            return services;
        }
    }
}
