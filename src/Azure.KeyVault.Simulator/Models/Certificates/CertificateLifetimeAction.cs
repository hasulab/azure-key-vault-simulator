namespace Azure.KeyVault.Simulator.Models.Certificates
{
    public class CertificateLifetimeAction
    {
        public CertificateTrigger trigger { get; set; }
        public CertificateAction action { get; set; }
    }


}
