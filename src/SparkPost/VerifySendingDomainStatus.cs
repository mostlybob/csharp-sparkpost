using Newtonsoft.Json;
namespace SparkPost
{
    public class VerifySendingDomainStatus : SendingDomainStatus
    {
        [JsonProperty("dns")]
        public Dns Dns { get; set; }
    }
}