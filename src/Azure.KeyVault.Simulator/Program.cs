using System.Net;
using Azure.Security.KeyVault.Secrets;
using Azure;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/secrets/{key}", (string key) =>
{
    return new KeyVaultSecret(key, "test value");
});

app.Run();
