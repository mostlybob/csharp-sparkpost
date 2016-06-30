using Newtonsoft.Json;
namespace SparkPost
{
    public class UpdateSendingDomainResponse : Response
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("dkim")]
        public Dkim Dkim { get; set; }
    }
}