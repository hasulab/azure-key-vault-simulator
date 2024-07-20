using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc.Testing;

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

    public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/secrets/SampleKey")]
        [InlineData("/certificates/testCert")]
        [InlineData("/keys/testKey")]
        public async Task Get_secrets(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }
    }
}