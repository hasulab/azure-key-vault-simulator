namespace Azure.KeyVault.Simulator.Extensions
{
    public static class DatetimeExtensions
    {
        public static double ToUnixTime(this DateTime input)
        {
            return input.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
