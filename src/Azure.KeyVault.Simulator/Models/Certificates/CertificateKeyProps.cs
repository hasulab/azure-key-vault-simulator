namespace Azure.KeyVault.Simulator.Models.Certificates
{
    public class CertificateKeyProps
    {
        public bool exportable { get; set; }
        public string kty { get; set; }
        public int key_size { get; set; }
        public bool reuse_key { get; set; }
    }


}
