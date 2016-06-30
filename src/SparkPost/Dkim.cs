using Newtonsoft.Json;
namespace SparkPost
{
    public class Dkim
    {
        [JsonProperty("signing_domain")]
        public string SigningDomain { get; set; }

        [JsonProperty("private")]
        public string PrivateKey { get; set; }

        [JsonProperty("public")]
        public string PublicKey { get; set; }

        [JsonProperty("selector")]
        public string Selector { get; set; }

        [JsonProperty("headers")]
        public string Headers { get; set; }
    }
}