using Azure.KeyVault.Simulator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Azure.KeyVault.Simulator.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapKeyVaultSimulatorEndpoint(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("/secrets/{key}", (string key, [FromServices] KeyVaultConfig config, [FromServices] ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("secrets");
                var secret = config.Secrets.FirstOrDefault(x => x.Key == key);
                if (string.IsNullOrEmpty(secret.Key))
                {
                    logger.LogError("{key} no found", key);
                    return Results.NotFound();
                }

                logger.LogInformation("Found {key}", key);

                return Results.Ok(new KeyVaultSecret
                {
                    Name = key,
                    Value = secret.Value,
                    Id = $"https://keyvttest.vault.azure.net/secrets/{key}",
                    Attributes = new KeyVaultSecretAttributes
                    {
                        Enabled = true,
                        Created = DateTime.Today.ToUnixTime(),
                        Updated = DateTime.Today.ToUnixTime(),
                        RecoveryLevel = "Recoverable+Purgeable"
                    }
                });
            });

        }
    }
}
