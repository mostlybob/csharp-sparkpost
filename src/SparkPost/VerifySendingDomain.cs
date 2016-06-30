using Newtonsoft.Json;
namespace SparkPost
{
    public class VerifySendingDomain
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("spf_verify")]
        public bool SpfVerify { get; set; }

        [JsonProperty("dkim_verify")]
        public bool DkimVerify { get; set; }

        [JsonProperty("postmaster_at_verify")]
        public bool PostmasterAtVerify { get; set; }

        [JsonProperty("postmaster_at_token")]
        public string PostmasterAtToken { get; set; }

        [JsonProperty("abuse_at_token")]
        public string AbuseAtToken { get; set; }
    }
}