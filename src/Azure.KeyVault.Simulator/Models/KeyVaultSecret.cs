namespace Azure.KeyVault.Simulator.Models
{
    public class KeyVaultSecret
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Id { get; set; }
        public KeyVaultSecretAttributes Attributes { get; set; }
    }
}
