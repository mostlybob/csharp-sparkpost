using Newtonsoft.Json;
namespace SparkPost
{
    public class Dns
    {
        [JsonProperty("dkim_record")]
        public string DkimRecord { get; set; }

        [JsonProperty("spf_record")]
        public string SpfRecord { get; set; }

        [JsonProperty("dkim_error")]
        public string DkimError { get; set; }

        [JsonProperty("spf_error")]
        public string SpfError { get; set; }
    }
}