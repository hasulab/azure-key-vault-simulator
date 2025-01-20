using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using System.Security.Cryptography.X509Certificates;

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
    }
}