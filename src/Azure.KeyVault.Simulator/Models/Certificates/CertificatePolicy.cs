namespace Azure.KeyVault.Simulator.Models.Certificates
{
    public class CertificatePolicy
    {
        public string id { get; set; }
        public CertificateKeyProps key_props { get; set; }
        public CertificateSecretProps secret_props { get; set; }
        public CertificateX509Props x509_props { get; set; }
        public List<CertificateLifetimeAction> lifetime_actions { get; set; }
        public CertificateIssuer issuer { get; set; }
        public CertificateAttributes attributes { get; set; }
    }


}
