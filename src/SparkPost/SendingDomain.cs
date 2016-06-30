using Newtonsoft.Json;
namespace SparkPost
{
    public class SendingDomain
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("tracking_domain")]
        public string TrackingDomain { get; set; }

        [JsonProperty("status")]
        public SendingDomainStatus Status { get; set; }

        [JsonProperty("dkim")]
        public Dkim Dkim { get; set; }

        [JsonProperty("generate_dkim")]
        public bool GenerateDkim { get; set; }

        [JsonProperty("dkim_key_length")]
        public long DkimKeyLength { get; set; }

        [JsonProperty("shared_with_subaccounts")]
        public bool SharedWithSubAccounts { get; set; }
    }
}