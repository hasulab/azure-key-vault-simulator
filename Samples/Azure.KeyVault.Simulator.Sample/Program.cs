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
builder.Services.AddKeyVaultSimulatorServices(builder.Configuration);

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.MapGet("/", () => "Hello World!");
//, [FromServices]ILogger logger
app.MapKeyVaultSimulatorEndpoint();
app.Run();
