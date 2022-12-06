# azure-key-vault-simulator
Azure Key Vault Simulator
## Getting Started 

* Create a new ASP.NET  Code empty Project
* Add latest [Hasulab.Azure.KeyVault.Simulator nuget library](https://www.nuget.org/packages/Hasulab.Azure.KeyVault.Simulator/0.0.3#readme-body-tab) version
* Add following lines to Progarm.cs
```
builder.Services.AddLogging();
builder.Services.AddKeyVaultSimulatorServices(builder.Configuration);

//Other codes
app.MapKeyVaultSimulatorEndpoint();

```