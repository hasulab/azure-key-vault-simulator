using Azure.KeyVault.Simulator.Models;
using Azure.KeyVault.Simulator.Models.Certificates;
using Azure.KeyVault.Simulator.Models.Keys;
using Azure.KeyVault.Simulator.Models.Secret;
using Microsoft.AspNetCore.Mvc;

namespace Azure.KeyVault.Simulator.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapKeyVaultSimulatorEndpoint(this IEndpointRouteBuilder endpoint)
        {
            //https://learn.microsoft.com/en-us/rest/api/keyvault/secrets/get-secret/get-secret?tabs=HTTP
            //GET {vaultBaseUrl}/secrets/{secret-name}/{secret-version}?api-version=7.3
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
                    Attributes = BuildKeyVaultAttributes()
                });
            });

            //https://learn.microsoft.com/en-us/rest/api/keyvault/certificates/get-certificate/get-certificate?source=recommendations&tabs=HTTP
            //GET {vaultBaseUrl}/certificates/{certificate-name}/{certificate-version}?api-version=7.3
            endpoint.MapGet("/certificates/{name}",
                (string name, [FromServices] KeyVaultConfig config, [FromServices] ILoggerFactory loggerFactory) =>
                {
                    var response = new CertificateResponse()
                    {
                        attributes = BuildKeyVaultAttributes()
                    };
                    return Results.Ok(response);
                });

            //https://learn.microsoft.com/en-us/rest/api/keyvault/keys/get-key/get-key?tabs=HTTP
            //GET {vaultBaseUrl}/keys/{key-name}/{key-version}?api-version=7.3
            endpoint.MapGet("/keys/{name}",
                (string name, [FromServices] KeyVaultConfig config, [FromServices] ILoggerFactory loggerFactory) =>
                {
                    var response = new KeyResponse
                    {
                        attributes= BuildKeyVaultAttributes()
                    };
                    return Results.Ok(response);
                });
        }

        private static KeyVaultAttributes BuildKeyVaultAttributes()
        {
            return new KeyVaultAttributes
            {
                enabled = true,
                created = DateTime.Today.ToUnixTime(),
                updated = DateTime.Today.ToUnixTime(),
                recoveryLevel = "Recoverable+Purgeable"
            };
        }
    }
}
