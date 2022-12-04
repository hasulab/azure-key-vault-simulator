namespace Azure.KeyVault.Simulator.Models.Certificates
{

    public class CertificateX509Props
    {
        public string subject { get; set; }
        public List<object> ekus { get; set; }
        public List<object> key_usage { get; set; }
        public int validity_months { get; set; }
    }


}
