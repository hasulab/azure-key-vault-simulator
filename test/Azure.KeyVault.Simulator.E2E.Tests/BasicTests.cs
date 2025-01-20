using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Azure.KeyVault.Simulator.E2E.Tests
{
    public class BasicTests
    {
        //string akvUrl = "https://akv-test.vault.azure.net/";
        string akvUrl = "https://localhost:5006/";

        [Fact]
        public async Task TestSecret()
        {
            var secretClient = new SecretClient(new Uri(akvUrl), new DefaultAzureCredential());
            var secret = await secretClient.GetSecretAsync("SampleKey");
            Assert.Equal(secret.Value.Value, "SampleValue");
        }

        //

        [Fact]
        public async Task TestCertificate()
        {
            var certificateClient = new CertificateClient(new Uri(akvUrl), new DefaultAzureCredential());
            var certificateWithPolicy = await certificateClient.GetCertificateAsync("localhostcaroot");
            Assert.NotNull(certificateWithPolicy.Value);
            var x509Certificate1 = new X509Certificate2(certificateWithPolicy.Value.Cer);

            var secretClient = new SecretClient(new Uri(akvUrl), new DefaultAzureCredential());
            var certificateSecret = await secretClient.GetSecretAsync("localhostcaroot");
            Assert.NotNull(certificateSecret.Value.Value);

            byte[] pfxBytes = Convert.FromBase64String(certificateSecret.Value.Value);

            var x509Certificate2 = new X509Certificate2(pfxBytes);

        }

        [Fact] // not working
        public async Task TestCertificateWithHttp() 
        {
            string keyVaultUrl = "https://<YourKeyVaultName>.vault.azure.net/";
            string certificateName = "<YourCertificateName>";
            string apiVersion = "7.3"; // Key Vault API version

            keyVaultUrl = "https://akv-test.vault.azure.net/";
            certificateName = "localhostcaroot";

            // Obtain an access token using DefaultAzureCredential
            var credential = new DefaultAzureCredential();
            var token = await credential.GetTokenAsync(
                new Azure.Core.TokenRequestContext(new[] { "https://vault.azure.net/.default" })
            );

            // Use HttpClient to fetch the certificate secret
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);

                // Fetch the certificate secret
                string secretUrl = $"{keyVaultUrl}secrets/{certificateName}?api-version={apiVersion}";
                HttpResponseMessage response = await httpClient.GetAsync(secretUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var secretResponse = JsonSerializer.Deserialize<SecretResponse>(responseBody);

                    // Decode the Base64-encoded PFX value
                    byte[] pfxBytes = Convert.FromBase64String(secretResponse.Value);

                    // Save the PFX file
                    string filePath = $"{certificateName}.pfx";
                    await File.WriteAllBytesAsync(filePath, pfxBytes);
                    Console.WriteLine($"Certificate saved to: {filePath}");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                }
            }
        }

        // Helper class to deserialize the secret response
        public class SecretResponse
        {
            public string Value { get; set; }
        }
    }
}