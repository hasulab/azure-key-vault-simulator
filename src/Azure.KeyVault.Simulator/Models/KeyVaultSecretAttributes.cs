namespace Azure.KeyVault.Simulator.Models;

public class KeyVaultSecretAttributes
{
    public bool Enabled { get; set; }
    public double Created { get; set; }
    public double Updated { get; set; }
    public string RecoveryLevel { get; set; }
}