# azure-key-vault-simulator
Azure Key Vault Simulator
## Getting Started 

* Create a new ASP.NET Code empty Project
* Add latest [Hasulab.Azure.KeyVault.Simulator nuget library](https://www.nuget.org/packages/Hasulab.Azure.KeyVault.Simulator/0.0.3#readme-body-tab) version
* Add following lines to Progarm.cs
```
builder.Services.AddLogging();
builder.Services.AddKeyVaultSimulatorServices(builder.Configuration);

//Other codes
app.MapKeyVaultSimulatorEndpoint();

```
* Add following sample  Secret key/values to appsettings.json or appsettings.Development.json

```
  "KeyVaultConfig": {
    "Secrets": {
      "SampleKey": "SampleValue", 
      "SampleCosmosDBConnectionString": "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
      "SampleStorageAccountKey": "UseDevelopmentStorage=true"
    }
  } 
```
* Press F5 or run the proejct
* Open http://[url with your port]/Secrets/[key] e.g. http://localhost:5005/Secrets/SampleCosmosDBConnectionString
