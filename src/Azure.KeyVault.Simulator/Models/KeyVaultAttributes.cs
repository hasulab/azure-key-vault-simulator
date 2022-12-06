namespace Azure.KeyVault.Simulator.Models;

public class KeyVaultAttributes
{
    public bool enabled { get; set; }
    public double created { get; set; }
    public double updated { get; set; }
    public string recoveryLevel { get; set; }
}
public class KeyVaultAttributesExtra: KeyVaultAttributes
{
    public double nbf { get; set; }
    public double exp { get; set; }
}