using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace Azure.KeyVault.Simulator.E2E.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var secretClient = new SecretClient(new Uri("https://localhost:5006/"), new DefaultAzureCredential());
            var secret = await secretClient.GetSecretAsync("SampleKey");
            Assert.Equal(secret.Value.Value, "SampleValue");
        }
    }
}