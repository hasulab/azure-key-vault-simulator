namespace Azure.KeyVault.Simulator.Models.Secret
{
    public class KeyVaultSecret
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Id { get; set; }
        public KeyVaultAttributes Attributes { get; set; }
    }
}
