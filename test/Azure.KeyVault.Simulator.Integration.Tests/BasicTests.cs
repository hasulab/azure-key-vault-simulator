using Microsoft.AspNetCore.Mvc.Testing;

namespace Azure.KeyVault.Simulator.Integration.Tests
{
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