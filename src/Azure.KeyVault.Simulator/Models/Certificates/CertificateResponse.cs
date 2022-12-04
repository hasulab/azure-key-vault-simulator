namespace Azure.KeyVault.Simulator.Models.Certificates
{
    public class CertificateResponse
    {
        public string id { get; set; }
        public string kid { get; set; }
        public string sid { get; set; }
        public string x5t { get; set; }
        public string cer { get; set; }
        public KeyVaultAttributes attributes { get; set; }
        public CertificatePolicy policy { get; set; }
    }


}
