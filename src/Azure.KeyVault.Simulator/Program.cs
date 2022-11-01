using System.Net;
using Azure;
using MediatR;
using System.Reflection;
using Azure.KeyVault.Simulator.Extensions;
using Azure.KeyVault.Simulator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.Configure<KeyVaultConfig>(settings =>
{
    builder.Configuration.GetSection(nameof(KeyVaultConfig)).Bind(settings);
});

var settings = new KeyVaultConfig();
builder.Configuration.GetSection(nameof(KeyVaultConfig)).Bind(settings);
builder.Services.AddSingleton<KeyVaultConfig>(settings);

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.MapGet("/", () => "Hello World!");
//, [FromServices]ILogger logger
app.MapGet("/secrets/{key}", (string key, [FromServices] KeyVaultConfig config, [FromServices] ILoggerFactory loggerFactory) =>
{
    var logger= loggerFactory.CreateLogger("secrets");
    var secret = config.Secrets.FirstOrDefault(x => x.Key == key);
    if (string.IsNullOrEmpty(secret.Key))
    {
        logger.LogError("{key} no found", key);
        return Results.NotFound();
    }

    logger.LogInformation("Found {key}",key);

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

app.Run();
