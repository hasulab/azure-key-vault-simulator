using Newtonsoft.Json;

namespace Azure.KeyVault.Simulator.Models.Keys
{
    public class KeyResponse
    {
        public KeyModel key { get; set; }
        public KeyVaultAttributes attributes { get; set; }
        public KeyTags tags { get; set; }
    }


    public class KeyModel
    {
        public string kid { get; set; }
        public string kty { get; set; }
        public List<string> key_ops { get; set; }
        public string n { get; set; }
        public string e { get; set; }
    }


    public class KeyTags
    {
        public string purpose { get; set; }

        [JsonProperty("test name ")]
        public string testname { get; set; }
    }





}
